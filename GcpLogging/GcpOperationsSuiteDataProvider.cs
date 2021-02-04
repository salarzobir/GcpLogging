using Audit.Core;
using Google.Cloud.Diagnostics.AspNetCore3;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Text.Json;

namespace GcpLogging
{
    public class GcpOperationsSuiteDataProvider : AuditDataProvider
    {
        private readonly IServiceProvider _serviceProvider;

        public GcpOperationsSuiteDataProvider(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public override object InsertEvent(AuditEvent auditEvent)
        {
            GoogleLoggerProvider googleLoggerProvider = GoogleLoggerProvider.Create(_serviceProvider, "gcplogging");
            ILogger logger = googleLoggerProvider.CreateLogger("Audit.NET");

            logger.LogInformation(JsonSerializer.Serialize(auditEvent));
            return null;
        }
    }
}
