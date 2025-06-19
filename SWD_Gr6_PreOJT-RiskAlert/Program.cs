using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Repositories.DBContext;
using Repositories.Interfaces;
using Repositories.Repositories;
using Services.Implement;
using Services.Interface;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<SemesterRepository>();
builder.Services.AddScoped<ISemesterService, SemesterService>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IAuthService, AuthService>();

// JWT Authentication
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = builder.Configuration["Jwt:Issuer"],
            ValidAudience = builder.Configuration["Jwt:Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))
        };
    });

builder.Services.AddTransient<SqlConnection>(_ =>
                new SqlConnection(builder.Configuration.GetConnectionString("RiskAlertDBConnection")));
builder.Services.AddDbContext<RiskAlertDBContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("RiskAlertDBConnection")));

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowLocalhost5195",
        builder =>
        {
            builder.WithOrigins("http://localhost:5195")
                   .AllowAnyMethod()
                   .AllowAnyHeader();
        });
});


var app = builder.Build();

app.UseCors("AllowLocalhost5195");

// Configure the HTTP request pipeline.
app.UseSwagger();
app.UseSwaggerUI(options =>
{
    options.SwaggerEndpoint("/swagger/v1/swagger.json", "Risk Alert API V1");
    options.RoutePrefix = string.Empty;
});
app.UseCors("AllowAll");

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
