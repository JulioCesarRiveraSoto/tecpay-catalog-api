using AutoMapper;
using FluentValidation;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;
using TecPay.Catalog.API.Middleware;
using TecPay.Catalog.Application.Entities.Categorias.DTOs;
using TecPay.Catalog.Application.Entities.Categorias.Mapping;
using TecPay.Catalog.Application.Entities.Categorias.Validators;
using TecPay.Catalog.Application.Entities.Productos;
using TecPay.Catalog.Application.Entities.Productos.DTOs;
using TecPay.Catalog.Application.Entities.Productos.Mapping;
using TecPay.Catalog.Application.Entities.Productos.Validators;
using TecPay.Catalog.Infrastructure;
using TecPay.Catalog.Infrastructure.Persistence;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen(options =>
{
    // Información general del API
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "TecPay Catalog API",
        Version = "v1",
        Description = "API Full Stack (.NET 9 + Angular 20) para prueba técnica."
    });

    // Definición del esquema Bearer JWT
    options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        Scheme = "bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description =
            "Ingrese el token JWT.\n\n" +
            "Ejemplo:\n\n" +
            "Bearer eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9..."
    });

    // Aplicar seguridad global a todos los endpoints protegidos
    options.AddSecurityRequirement(new OpenApiSecurityRequirement
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
            Array.Empty<string>()
        }
    });
});

builder.Services.AddApiVersioning(o =>
{
    o.DefaultApiVersion = new Asp.Versioning.ApiVersion(1, 0);
    o.AssumeDefaultVersionWhenUnspecified = true;
    o.ReportApiVersions = true;
});

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAngular", policy =>
    {
        policy.WithOrigins("http://localhost:4200")
              .AllowAnyMethod()
              .AllowAnyHeader()
              .WithExposedHeaders("cantidadTotalRegistros");
    });
});

builder.Services.AddMediatR(cfg =>
    cfg.RegisterServicesFromAssembly(typeof(ProductoDTO).Assembly));

builder.Services.AddAutoMapper(cfg =>
{
    cfg.AddProfile<ProductoProfile>();
    cfg.AddProfile<CategoriaProfile>();
});

builder.Services.AddScoped<IValidator<ProductoCreacionDTO>, ProductoCreacionValidator>();
builder.Services.AddScoped<IValidator<CategoriaCreacionDTO>, CategoriaCreacionValidator>();

builder.Services.AddInfrastructure(builder.Configuration);

var jwt = builder.Configuration.GetSection("Jwt");

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(o =>
    {
        o.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = jwt["Issuer"],
            ValidAudience = jwt["Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(
                    Encoding.UTF8.GetBytes(jwt["Key"]!))
        };
});

builder.Services.AddAuthorization(o =>
    o.AddPolicy("CatalogWrite", p => p.RequireRole("Admin")));

var app = builder.Build();

app.UseMiddleware<ExceptionMiddleware>();

if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseCors("AllowAngular");
app.UseStaticFiles();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();

using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<CatalogDbContext>();
    await DbSeeder.SeedAsync(db);
}

app.Run();

public partial class Program { }