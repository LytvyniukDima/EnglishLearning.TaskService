using EnglishLearning.TaskService.Application.Configuration;
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
                        .AllowCredentials()
                        .WithExposedHeaders("Authorization"));
            });

            services.AddMvc(options => options.AddEnglishLearningIdentityFilters());

            services.AddSwaggerDocumentation();
            
            services.PersistenceConfiguration(Configuration)
                .ApplicationConfiguration()
                .WebConfiguration();

            services
                .AddRedis(Configuration)
                .AddEnglishLearningIdentity();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            
            app.UseEnglishLearningExceptionMiddleware();
                
            app.UseCors("CorsPolicy");
            app.UseHttpsRedirection();

            app.UseSwaggerDocumentation();
            
            app.UseMvc();
        }
    }
}