using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Serilog;
using Serilog.Sinks.SystemConsole.Themes;
using System.Text;

namespace GetDataService
{
    public class Program
    {

        public static ILogger Log = null;

        public static void Main(string[] args)
        {

            Log = new LoggerConfiguration()
           .WriteTo.Console(theme: SystemConsoleTheme.Colored)
           .WriteTo.Async(w => w.File("Logs/log-.log", rollingInterval: RollingInterval.Day))
           .MinimumLevel.Debug()
           .CreateLogger();

            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
           .UseSystemd()
                .ConfigureServices((hostContext, services) =>
                {
                    services.AddHostedService<Worker>();
                }).UseSerilog();
    }
}
