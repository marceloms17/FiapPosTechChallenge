using Asp.Versioning.ApiExplorer;
using Core.PosTech8Nett.Api.Infra.Swagger;
using Core.PosTech8Nett.Api.Infra.Versioning;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddVersioning();
builder.Services.AddSwaggerDocumentation();


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

app.Run();
