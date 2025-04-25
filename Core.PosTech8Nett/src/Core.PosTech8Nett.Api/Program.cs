using Asp.Versioning.ApiExplorer;
using Core.PosTech8Nett.Api.Infra.Auth;
using Core.PosTech8Nett.Api.Infra.Data;
using Core.PosTech8Nett.Api.Infra.Swagger;
using Core.PosTech8Nett.Api.Infra.Versioning;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Core.PosTech8Nett.Api.Infra.Logs;
using Asp.Versioning.ApiExplorer;
using Serilog;
using Microsoft.Extensions.Hosting;
using Serilog.Exceptions;
using System.Linq;
using Serilog.Events;

var builder = WebApplication.CreateBuilder(args);

//builder.Host.UseSerilog((context, logger) =>
//{
//    logger.Enrich.FromLogContext();
//    logger.Enrich.WithExceptionDetails();
//    logger.Enrich.WithMachineName();
//    logger.MinimumLevel.Override("Microsoft", Serilog.Events.LogEventLevel.Warning);
//    logger.MinimumLevel.Override("System", Serilog.Events.LogEventLevel.Warning);
//    logger.Filter.ByExcluding(c => c.Properties.Any(p => p.Value.ToString().Contains("/ready")));
//    logger.Filter.ByExcluding(c => c.Properties.Any(p => p.Value.ToString().Contains("/liveness")));
//    logger.Filter.ByExcluding(c => c.Properties.Any(p => p.Value.ToString().Contains("/hc")));
//    logger.ReadFrom.Configuration(context.Configuration);
//});

builder.Services.AddSwaggerDocumentation();
builder.Services.AddAuthorizationExtension();

// Configurar o DbContext com SQL Server
builder.Services.AddDbContext<ApplicationContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddMvcCore(options => options.AddLogRequestFilter());
builder.Services.AddVersioning();

var app = builder.Build();

var apiVersionDescriptionProvider = app.Services.GetRequiredService<IApiVersionDescriptionProvider>();

app.UseSwagger();
app.UseSwaggerUI(options =>
{
    foreach (var description in apiVersionDescriptionProvider.ApiVersionDescriptions)
    {
        options.SwaggerEndpoint($"/swagger/{description.GroupName}/swagger.json", $"API {description.GroupName.ToUpperInvariant()}");
        options.RoutePrefix = "swagger";
    }
});

app.MapControllers();
app.UseAuthentication();
app.UseAuthorization();
app.UseCorrelationId();

app.Run();
