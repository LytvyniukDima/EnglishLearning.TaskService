using System;
using System.Text.Json.Serialization;
using EnglishLearning.TaskService.Application.Configuration;
using EnglishLearning.TaskService.BackgroundJobs.Configuration;
using EnglishLearning.TaskService.EventHandlers.Configuration;
using EnglishLearning.TaskService.Host.Infrastructure;
using EnglishLearning.TaskService.Persistence.Configuration;
using EnglishLearning.TaskService.Web.Configuration;
using EnglishLearning.Utilities.General.Extensions;
using EnglishLearning.Utilities.Identity.Configuration;
using EnglishLearning.Utilities.Persistence.Redis.Configuration;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace EnglishLearning.TaskService.Host
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
            services.AddCors(options =>
            {
                options.AddPolicy(
                    "CorsPolicy",
                    builder => builder.AllowAnyOrigin()
                        .AllowAnyMethod()
                        .AllowAnyHeader()
                        .WithExposedHeaders("Authorization"));
            });

            services
                .AddControllers(options => options.AddEnglishLearningIdentityFilters())
                .AddJsonOptions(options =>
                {
                    options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
                });

            services.AddSwaggerDocumentation();

            services
                .PersistenceConfiguration(Configuration)
                .ApplicationConfiguration()
                .WebConfiguration()
                .BackgroundJobsConfiguration(Configuration)
                .AddEventHandlerConfiguration(Configuration);

            services
                .AddRedis(Configuration)
                .AddEnglishLearningIdentity();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IServiceProvider serviceProvider, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            
            app.UseEnglishLearningExceptionMiddleware();
                
            app.UseCors("CorsPolicy");
            app.UseHttpsRedirection();

            app.UseSwaggerDocumentation();

            app.UseBackgroundJobsServices(serviceProvider);
            
            app.UseRouting();
            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
        }
    }
}
