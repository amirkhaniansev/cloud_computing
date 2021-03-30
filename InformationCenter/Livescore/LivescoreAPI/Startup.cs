using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using LivescoreAPI.Exceptions;
using LivescoreDAL.Factories;
using LivescoreDAL.Parameters;
using LivescoreAPI.Constants;

namespace LivescoreAPI
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            this.Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "LivescoreAPI", Version = "v1" });
            });

            services.AddSingleton(
                new DalFactory(
                    new DatabaseConfiguration
                    {
                        IsTest = bool.Parse(this.Configuration[SettingKeys.IsTest]),
                        DatabaseName = this.Configuration[SettingKeys.LivescoreDB],
                        ConnectionString = this.Configuration.GetConnectionString(SettingKeys.LivescoreDB)
                    }));
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "LivescoreAPI v1"));
            }

            app.UseRouting();
            app.UseAuthorization();
            app.UseMiddleware(typeof(ExceptionHandler));
            
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
