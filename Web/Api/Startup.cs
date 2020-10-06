using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using Microsoft.AspNetCore.Cors.Infrastructure;
using Api.Factories;
using Protos;
using Protos.Client;
using MassTransit;
using Messages;

namespace Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton(new GrpcChannelFactory<Weather.WeatherClient>(Configuration["Grpc:WeatherClientAddress"]));
            services.AddScoped(x => x.GetService<GrpcChannelFactory<Weather.WeatherClient>>().CreateChannel());

            services.AddControllers();
            services.AddCors(options =>
            {


                options.AddPolicy("dev", builder =>
                {
                    builder.AllowAnyOrigin();
                    builder.AllowAnyHeader();
                    builder.AllowAnyMethod();
                });
            });
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "api", Version = "v1" });
            });

            services.AddMassTransit(x =>
            {
                x.UsingRabbitMq((context, cfg) => cfg.Host("localhost"));

                var timeout = TimeSpan.FromSeconds(10);
                var serviceAddress = new Uri("rabbitmq://localhost/weather");
                x.AddRequestClient<AddWeatherForecast>(serviceAddress, timeout);
                x.AddRequestClient<RemoveWeatherForecast>(serviceAddress, timeout);
            });
            services.AddMassTransitHostedService();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "api v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseCors("dev");

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
