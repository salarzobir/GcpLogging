using Google.Cloud.Diagnostics.AspNetCore3;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using System;

namespace GcpLogging
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>()
                              .UseGoogleDiagnostics("gcplogging")
                              .UsePortEnvironmentVariable();
                });
    }

    internal static class ProgramExtensions
    {
        // Google Cloud Run sets the PORT environment variable to tell this
        // process which port to listen to.
        public static IWebHostBuilder UsePortEnvironmentVariable(this IWebHostBuilder webBuilder)
        {
            string port = Environment.GetEnvironmentVariable("PORT");
            if (!string.IsNullOrEmpty(port))
            {
                webBuilder.UseUrls($"https://0.0.0.0:{port}", $"http://0.0.0.0:{port}");
            }
            return webBuilder;
        }
    }
}
