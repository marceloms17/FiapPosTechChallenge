using Asp.Versioning.ApiExplorer;
using Core.PosTech8Nett.Api.Infra.Auth.Extension;
using Core.PosTech8Nett.Api.Infra.DataBase.EntityFramework.Context;
using Core.PosTech8Nett.Api.Infra.Identity.Extension;
using Core.PosTech8Nett.Api.Infra.Logs;
using Core.PosTech8Nett.Api.Infra.Logs.Extension;
using Core.PosTech8Nett.Api.Infra.Migration.Extension;
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
builder.AddAuthorizationExtension();

builder.Services.AddMvcCore(options => options.AddLogRequestFilter());
builder.Services.AddVersioning();
builder.Services.AddSwaggerDocumentation();
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddIdentityExtension();
builder.Services.AddIoC();

var app = builder.Build();

app.ExecuteMigrations();
var apiVersionDescriptionProvider = app.Services.GetRequiredService<IApiVersionDescriptionProvider>();
app.UseVersionedSwagger(apiVersionDescriptionProvider);
app.MapControllers();
app.UseAuthentication();
app.UseAuthorization();
app.UseCorrelationId();

app.Run();
