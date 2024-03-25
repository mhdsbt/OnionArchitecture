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


            var _rabbitMqConfigurations = new MessageBusConfiguration();
            Configuration.GetSection("RabbitMQ").Bind(_rabbitMqConfigurations);


            services.AddCap(options =>
            {

                options.UseMySql(Configuration.GetConnectionString("DefaultConnection"));
                options.UseDashboard();

                options.UseRabbitMQ(opt =>
                {
                    opt.HostName = _rabbitMqConfigurations.Host;
                    opt.UserName = _rabbitMqConfigurations.Username;
                    opt.Password = _rabbitMqConfigurations.Password;
                    opt.VirtualHost = _rabbitMqConfigurations.VirtualHost;
                    opt.Port = _rabbitMqConfigurations.Port;
                //    opt.CustomHeaders = e => new List<KeyValuePair<string, string>>
                //{
                //    new KeyValuePair<string, string>(Headers.MessageId, Guid.NewGuid().ToString()),
                //    new KeyValuePair<string, string>(Headers.MessageName, e.RoutingKey),
                //};
                //    opt.BasicQosOptions = new RabbitMQOptions.BasicQos(1);
                });
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