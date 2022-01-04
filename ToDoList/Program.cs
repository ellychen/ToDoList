using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Serilog;

namespace ToDoList
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
            if (!string.IsNullOrWhiteSpace(args.FirstOrDefault()))
            {
                environment = args.First();
            }                       
            // Serilog �]�w
            //�q appsettings.json Ū���]�w���
            var configBuilder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile(path: "appsettings.json", optional: false, reloadOnChange: true);
            //
            if (environment != "") configBuilder.AddJsonFile(path: $"appsettings.{environment}.json", optional: true, reloadOnChange: true);

            var configuration = configBuilder.Build();

            //�ϥαq appsettings.json Ū���쪺���e�ӳ]�w logger
            Log.Logger = new LoggerConfiguration()
                .ReadFrom.Configuration(configuration)
                .WriteTo.Console()
                .CreateLogger();

            var builder = CreateHostBuilder(args).Build();
            //�i�H��Ƶ{��

            builder.Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args)
        {
            return Host.CreateDefaultBuilder(args)
                .ConfigureAppConfiguration((context, builder) =>
                {
                })
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                })
                .UseSerilog();
        }

    }
}
