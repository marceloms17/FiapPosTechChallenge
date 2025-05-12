using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics.CodeAnalysis;
using System;
using Core.PosTech8Nett.Api.Infra.Logs.Filter;
using Core.PosTech8Nett.Api.Infra.Logs.Middleware;

namespace Core.PosTech8Nett.Api.Infra.Logs.Extension
{
    [ExcludeFromCodeCoverage]
    public static class LogRequestExtensions
    {
        public static IApplicationBuilder UseSimpleLogRequest(this IApplicationBuilder app)
        {
            if (app == null)
            {
                throw new ArgumentNullException(nameof(app));
            }
            return app.UseMiddleware<LogSimpleRequestMiddleware>();
        }

        public static MvcOptions AddLogRequestFilter(this MvcOptions op)
        {
            op.Filters.Add<LogRequestActionFilter>();
            
            return op;
        }
    }
}

