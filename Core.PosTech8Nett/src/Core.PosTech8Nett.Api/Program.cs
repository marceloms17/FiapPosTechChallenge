using Asp.Versioning.ApiExplorer;
using Core.PosTech8Nett.Api.Domain.Mappers;
using Core.PosTech8Nett.Api.Infra.Auth.Extension;
using Core.PosTech8Nett.Api.Infra.Auth.Middleware;
using Core.PosTech8Nett.Api.Infra.DataBase.EntityFramework.Context;
using Core.PosTech8Nett.Api.Infra.Identity.Extension;
using Core.PosTech8Nett.Api.Infra.Logs;
using Core.PosTech8Nett.Api.Infra.Logs.Extension;
using Core.PosTech8Nett.Api.Infra.Migration.Extension;
using Core.PosTech8Nett.Api.Infra.Monitoring;
using Core.PosTech8Nett.Api.Infra.Services.Extensions;
using Core.PosTech8Nett.Api.Infra.Swagger.Extension;
using Core.PosTech8Nett.Api.Infra.Swagger.Middleware;
using Core.PosTech8Nett.Api.Infra.Versioning.Extension;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

var builder = WebApplication.CreateBuilder(args);

builder.AddSerilogConfiguration();

builder.Services.AddMvcCore(options => options.AddLogRequestFilter());
builder.Services.AddVersioning();
builder.Services.AddSwaggerDocumentation();
builder.Services.AddAutoMapper(typeof(UsersEntitieMapperProfile));
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddIdentityExtension();
builder.Services.AddAuthorizationExtension(builder.Configuration);
builder.Services.AddIoC();

// Adiciona configuração CORS para permitir solicitações do Prometheus
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", builder =>
    {
        builder.AllowAnyOrigin()
               .AllowAnyMethod()
               .AllowAnyHeader();
    });
});

// Adiciona monitoramento com Prometheus
builder.Services.AddPrometheusMonitoring();
builder.Services.AddMetricsCollector();

var app = builder.Build();

app.ExecuteMigrations();
var apiVersionDescriptionProvider = app.Services.GetRequiredService<IApiVersionDescriptionProvider>();

app.UseAuthentication();                        // 1�: popula HttpContext.User
app.UseMiddleware<RoleAuthorizationMiddleware>(); // 2�: seu middleware
app.UseCorrelationId();

// Adiciona CORS antes de outros middlewares
app.UseCors("AllowAll");

// Adiciona middleware de monitoramento
app.UsePrometheusMonitoring();
app.UseMetricsMiddleware();

app.UseVersionedSwagger(apiVersionDescriptionProvider);
app.UseAuthorization();                         // 3�: aplica [Authorize]
app.MapControllers();
app.Run();