using Amazon.SimpleNotificationService;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.PlatformAbstractions;
using Newtonsoft.Json.Serialization;
using PlanningPoker.Interfaces.Services;
using PlanningPoker.Services;
using Swashbuckle.AspNetCore;
using Swashbuckle;
using Swashbuckle.AspNetCore.Swagger;

namespace PlanningPoker
{
    public class Startup
    {
        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
                .AddEnvironmentVariables();
            Configuration = builder.Build();
        }

        public IConfigurationRoot Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.Configure<Model.Configuration.Cors>(Configuration.GetSection("Cors"));

            services.AddSingleton<ISessionsService, SessionsService>();

            services.AddSingleton<ISnsService, SnsService>();

            // Add framework services.
            services.AddCors();
            services.AddMvc()
                .AddJsonOptions(options => options.SerializerSettings.ContractResolver = new DefaultContractResolver());
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info { Title = "My API", Version = "v1" });

                //Set the comments path for the swagger json and ui.
                var basePath = PlatformServices.Default.Application.ApplicationBasePath;
                var xmlPath = Path.Combine(basePath, "PlanningPoker.xml");
                c.IncludeXmlComments(xmlPath);
            });

            services.AddDefaultAWSOptions(Configuration.GetAWSOptions());
            services.AddAWSService<IAmazonSimpleNotificationService>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(
            IApplicationBuilder app, 
            IHostingEnvironment env, 
            ILoggerFactory loggerFactory,
            IOptions<Model.Configuration.Cors> corsOptionsAccessor
            )
        {
            var corsOptions = corsOptionsAccessor.Value;
            app.UseCors(builder =>
            {
                var origins = corsOptions.Origins;
                if (origins != null && origins.Length > 0)
                {
                    builder.WithOrigins(origins);
                }
                else
                {
                    builder.AllowAnyOrigin();
                }

                builder.AllowAnyHeader();
                builder.AllowAnyMethod();
            });

            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();

            app.UseMvc();

            // Enable middleware to serve generated Swagger as a JSON endpoint.
            app.UseSwagger();

            // Enable middleware to serve swagger-ui (HTML, JS, CSS etc.), specifying the Swagger JSON endpoint.
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
            });
        }
    }
}
