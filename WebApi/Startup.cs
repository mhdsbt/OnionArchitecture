using Application;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using DotNetCore.CAP;
using Humanizer.Configuration;
using DotNetCore.CAP.Persistence;
using DotNetCore.CAP.Messages;
using System;
using WebApi.Configuration;
using System.Text;
using Persistence.MessageConsumer;


//using DotNetCore.CAP.Persistence.EntityFrameworkCore;

namespace WebApi
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }


        public void ConfigureServices(IServiceCollection services)
        {
            #region Swagger
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "OnionArchitecture",
                });

            });
            #endregion


            services.AddApplication();
            services.AddPersistence(Configuration);
            services.AddApiVersioning();
            services.AddControllers();

            services.AddApiVersioning(options =>
            {
                options.DefaultApiVersion = new Microsoft.AspNetCore.Mvc.ApiVersion(1, 0);
                options.AssumeDefaultVersionWhenUnspecified = true;
            });

            var _rabbitMqConfigurations = new MessageBusConfiguration();
            Configuration.GetSection("RabbitMQ").Bind(_rabbitMqConfigurations);


            services.AddCap(options =>
            {
                
                options.UseRabbitMQ(opt =>
                {
                    opt.HostName = _rabbitMqConfigurations.Host;
                    opt.UserName = _rabbitMqConfigurations.Username;
                    opt.Password = _rabbitMqConfigurations.Password;
                    opt.VirtualHost = _rabbitMqConfigurations.VirtualHost;
                    opt.Port = _rabbitMqConfigurations.Port;
                    opt.ExchangeName = _rabbitMqConfigurations.Exchange;

                });
                
                options.FailedThresholdCallback = (info)
=>
                {
                    Console.Clear();
                    string encodedData = info.Message.Value.ToString();
                    string base64Part = encodedData.Substring(encodedData.IndexOf(',') + 1);
                    Console.WriteLine("Base64 Part: " + base64Part); // Log base64Part
                    try
                    {
                        byte[] decodedBytes = Convert.FromBase64String(base64Part);
                        string decodedString = Encoding.UTF8.GetString(decodedBytes);
                        Console.WriteLine("Error decoded string: " + decodedString);
                    }
                    catch (FormatException ex)
                    {
                        Console.WriteLine("Error decoding Base64 string: " + ex.Message);
                    }
                };
                options.UseDashboard();
                options.UseMySql(Configuration.GetConnectionString("DefaultConnection"));
            });
        }
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();


            app.UseAuthorization();
            #region Swagger
            // Enable middleware to serve generated Swagger as a JSON endpoint.
            app.UseSwagger();

            // Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.),
            // specifying the Swagger JSON endpoint.
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "OnionArchitecture");
            });
            #endregion
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}