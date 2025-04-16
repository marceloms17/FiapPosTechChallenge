using Asp.Versioning.ApiExplorer;
using Core.PosTech8Nett.Api.Infra.Auth;
using Core.PosTech8Nett.Api.Infra.Data;
using Core.PosTech8Nett.Api.Infra.Swagger;
using Core.PosTech8Nett.Api.Infra.Versioning;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddVersioning();
builder.Services.AddSwaggerDocumentation();
builder.Services.AddAuthorizationExtension();

// Configurar o DbContext com SQL Server
builder.Services.AddDbContext<ApplicationContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

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

app.Run();
