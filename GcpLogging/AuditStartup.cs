using Audit.Core;
using Audit.Mvc;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;

namespace GcpLogging
{
    public static class AuditStartup
    {
        private const string CorrelationIdField = "CorrelationId";

        /// <summary>
        /// Add the global audit filter to the MVC pipeline
        /// </summary>
        public static MvcOptions AddAudit(this MvcOptions mvcOptions)
        {
            mvcOptions.Filters.Add(new AuditAttribute()
            {
                EventTypeName = "MVC:{verb}:{controller}:{action}",
                IncludeHeaders = true,
                IncludeModel = true,
                IncludeRequestBody = true,
                IncludeResponseBody = true
            });
            return mvcOptions;
        }

        /// <summary>
        /// Global Audit configuration
        /// </summary>
        public static IServiceCollection ConfigureAudit(this IServiceCollection serviceCollection)
        {
            Audit.Core.Configuration.Setup()
                        .UseCustomProvider(new GcpOperationsSuiteDataProvider(serviceCollection.BuildServiceProvider()));

            return serviceCollection;
        }

        /// <summary>
        /// Add a RequestId so the audit events can be grouped per request
        /// </summary>
        public static void UseAuditCorrelationId(this IApplicationBuilder app, IHttpContextAccessor ctxAccesor)
        {
            Configuration.AddCustomAction(ActionType.OnScopeCreated, scope =>
            {
                HttpContext httpContext = ctxAccesor.HttpContext;
                scope.Event.CustomFields[CorrelationIdField] = httpContext.TraceIdentifier;
            });
        }
    }
}
