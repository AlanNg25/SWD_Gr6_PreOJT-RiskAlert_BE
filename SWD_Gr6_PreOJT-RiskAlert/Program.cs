﻿using Applications.Mapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Microsoft.SemanticKernel;
using Microsoft.SemanticKernel.ChatCompletion;
using Microsoft.SemanticKernel.Connectors.AzureOpenAI;
using Repositories.DBContext;
using Repositories.Interfaces;
using Repositories.Repositories;
using Services.Implement;
using Services.Interface;
using System.Text;

//Add Kernel and Azure OpenAI dependencies
var config = new ConfigurationBuilder()
    .AddUserSecrets<Program>()
    .Build();

string azureOpenAIApiEndpoint = config["azureOpenAIEndpoint"];
string azureOpenAIApiKey = config["azureOpenAIApiKey"];
string modelDeploymentName = config["modelDeploymentName"];

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Risk Alert API", Version = "v1" });
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Description = "Please enter JWT with Bearer into field",
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer"
    });
    c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            new string[] {}
        }
    });
});
//
builder.Services.AddAutoMapper(
    typeof(Program).Assembly,          // API
    typeof(MappingProfile).Assembly    // Applications
);

builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped<IServiceProviders, ServiceProviders>();
builder.Services.AddScoped<IAiService, AiService>();

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
    options.AddPolicy("AllowLocalhost5195", builder =>
    {
        builder.WithOrigins("http://localhost:5195")
               .AllowAnyMethod()
               .AllowAnyHeader();
    });
});

// Register IChatCompletionService as a singleton
builder.Services.AddSingleton<IChatCompletionService>(sp =>
{
    return new AzureOpenAIChatCompletionService(
        deploymentName: modelDeploymentName,
        endpoint: azureOpenAIApiEndpoint,
        apiKey: azureOpenAIApiKey
        );
});

// Register Kernel as a transient service
builder.Services.AddTransient<Kernel>(sp =>
{
    var kernelBuilder = Kernel.CreateBuilder();
    kernelBuilder.AddAzureOpenAIChatCompletion(
        deploymentName: modelDeploymentName,
        endpoint: azureOpenAIApiEndpoint,
        apiKey: azureOpenAIApiKey);
    return kernelBuilder.Build();
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint("/swagger/v1/swagger.json", "Risk Alert API V1");
        options.RoutePrefix = string.Empty;
    });
}

app.UseHttpsRedirection();

app.UseRouting();                       // 1. Định tuyến trước

app.UseCors("AllowLocalhost5195");      // (tùy, miễn trước Endpoint)

app.UseAuthentication();                // 2. Auth
app.UseAuthorization();                 // 3. Authorize  ✅ GIỮA Routing & Endpoints

app.MapControllers();                   // 4. Tạo Endpoints (thay cho UseEndpoints)

app.Run();