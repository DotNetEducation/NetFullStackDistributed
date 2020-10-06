using Domain.Services;
using MassTransit;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Repository.DbContexts;
using Repository.Interfaces;
using Repository.Repositories;
using System;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace Domain
{
    class Program
    {

        static async Task Main(string[] args)
        {
            var isService = !(Debugger.IsAttached || args.Contains("--console"));

            var builder = new HostBuilder()
                .ConfigureServices((hostContext, services) =>
                {
                    services.AddDbContext<WeatherDbContext>(options =>
                    {
                        options.UseSqlite("Data Source=../../../../Database/weather.db");
                    });
                    services.AddScoped<IWeatherForecastRepository, WeatherRespository>();


                    services.AddMassTransit(cfg =>
                    {
                        cfg.AddConsumer<WeatherService>();
                        cfg.AddBus(ConfigureBus);
                    });

                    services.AddHostedService<Startup>();
                })
                .ConfigureLogging((hostingContext, logging) =>
                {
                    logging.AddConsole();
                });

            if (isService)
            {
                await builder.UseWindowsService().Build().RunAsync();
                //await builder.UseSystemd().Build().RunAsync(); // For Linux, replace the nuget package: "Microsoft.Extensions.Hosting.WindowsServices" with "Microsoft.Extensions.Hosting.Systemd", and then use this line instead
            }
            else
            {
                await builder.RunConsoleAsync();
            }
        }

        static IBusControl ConfigureBus(IServiceProvider provider)
        {

            return Bus.Factory.CreateUsingRabbitMq(cfg =>
            {
                cfg.Host(new Uri("rabbitmq://localhost/"), h => { });

                cfg.ReceiveEndpoint("weather", e =>
                {
                    e.Consumer<WeatherService>(provider);
                });
                   
            });
        }

    }
}