using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Asp.Versioning;
using FluentValidation.AspNetCore;
using Microsoft.EntityFrameworkCore;
using InvoiceService.API.Data;
using InvoiceService.API.Interfaces;
using InvoiceService.API.Repositories;
using InvoiceService.API.Services;
using InvoiceService.API.Mappings;
using InvoiceService.API.Caching;

var builder = WebApplication.CreateBuilder(args);

// Controllers
builder.Services.AddControllers();

// Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// API Versioning
builder.Services.AddApiVersioning(options =>
{
    options.DefaultApiVersion = new ApiVersion(1, 0);
    options.AssumeDefaultVersionWhenUnspecified = true;
    options.ReportApiVersions = true;
});

// Fluent Validation
builder.Services.AddFluentValidationAutoValidation();

// AutoMapper
// AutoMapper
builder.Services.AddAutoMapper(typeof(InvoiceProfile));

// JWT Authentication
builder.Services
    .AddAuthentication(options =>
    {
        options.DefaultAuthenticateScheme =
            JwtBearerDefaults.AuthenticationScheme;

        options.DefaultChallengeScheme =
            JwtBearerDefaults.AuthenticationScheme;

        options.DefaultScheme =
            JwtBearerDefaults.AuthenticationScheme;
    })
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters =
            new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,

                ValidIssuer =
                    builder.Configuration["Jwt:Issuer"],

                ValidAudience =
                    builder.Configuration["Jwt:Audience"],

                IssuerSigningKey =
                    new SymmetricSecurityKey(
                        Encoding.UTF8.GetBytes(
                            builder.Configuration["Jwt:Key"]!
                        )
                    )
            };
    });

builder.Services.AddAuthorization();

// MySQL Database
builder.Services.AddDbContext<InvoiceDbContext>(options =>
    options.UseMySql(
        builder.Configuration.GetConnectionString("DefaultConnection"),
        ServerVersion.AutoDetect(
            builder.Configuration.GetConnectionString("DefaultConnection")
        )
    )
);

// Redis Cache
builder.Services.AddStackExchangeRedisCache(options =>
{
    options.Configuration = "localhost:6379";
    options.InstanceName = "InvoiceService_";
});

// Repository Registration
builder.Services.AddScoped<
    IInvoiceRepository,
    InvoiceRepository>();

// Service Registration
builder.Services.AddScoped<
    IInvoiceService,
    InvoiceService.API.Services.InvoiceService>();

builder.Services.AddHttpClient<
    ICustomerApiService,
    CustomerApiService>(client =>
{
    client.BaseAddress =
        new Uri("http://localhost:5266/");
});

// Cache Registration
builder.Services.AddScoped<
    ICacheService,
    RedisCacheService>();

var app = builder.Build();

// Swagger
app.UseSwagger();
app.UseSwaggerUI();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();