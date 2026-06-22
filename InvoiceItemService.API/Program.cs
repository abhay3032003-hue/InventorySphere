using Asp.Versioning;
using FluentValidation.AspNetCore;
using Microsoft.EntityFrameworkCore;
using InvoiceItemService.API.Data;
using InvoiceItemService.API.Interfaces;
using InvoiceItemService.API.Repositories;
using InvoiceItemService.API.Services;
using InvoiceItemService.API.Mappings;
using InvoiceItemService.API.Caching;

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
builder.Services.AddAutoMapper(typeof(InvoiceItemProfile));

// MySQL Database
builder.Services.AddDbContext<InvoiceItemDbContext>(options =>
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
    options.InstanceName = "InvoiceItemService_";
});

// Repository Registration
builder.Services.AddScoped<
    IInvoiceItemRepository,
    InvoiceItemRepository>();

// Service Registration
builder.Services.AddScoped<
    IInvoiceItemService,
    InvoiceItemService.API.Services.InvoiceItemService>();

// Cache Registration
builder.Services.AddScoped<
    ICacheService,
    RedisCacheService>();

var app = builder.Build();

// Swagger
app.UseSwagger();
app.UseSwaggerUI();

app.MapControllers();

app.Run();