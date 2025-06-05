using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Repositories.DBContext;
using Repositories.Repositories;
using Services.Implement;
using Services.Interface;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<ClassRepository>();
builder.Services.AddScoped<IClassService, ClassService>();

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
