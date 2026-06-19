using Asp.Versioning;
using FluentValidation.AspNetCore;
using Microsoft.EntityFrameworkCore;
using ProductService.API.Data;
using ProductService.API.Interfaces;
using ProductService.API.Repositories;
using ProductService.API.Services;
using ProductService.API.Caching;

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
builder.Services.AddAutoMapper(
    AppDomain.CurrentDomain.GetAssemblies());

// MySQL Database
builder.Services.AddDbContext<ProductDbContext>(options =>
    options.UseMySql(
        builder.Configuration.GetConnectionString("DefaultConnection"),
        ServerVersion.AutoDetect(
            builder.Configuration.GetConnectionString("DefaultConnection")
        )
    )
);

// Redis
builder.Services.AddStackExchangeRedisCache(options =>
{
    options.Configuration = "localhost:6379";
    options.InstanceName = "ProductService_";
});

// Repository Registration
builder.Services.AddScoped<
    IProductRepository,
    ProductRepository>();

// Service Registration
builder.Services.AddScoped<
    IProductService,
    ProductService.API.Services.ProductService>();


builder.Services.AddScoped<
    ICacheService,
    RedisCacheService>();

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

app.MapControllers();

app.Run();