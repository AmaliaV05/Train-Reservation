using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.SpaServices.AngularCli;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.FeatureManagement;
using System.Text.Json.Serialization;
using TrainReservation.Application.Helpers;
using TrainReservation.Extensions;
using TrainReservation.Middlewares;

namespace TrainReservation
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
            services.AddHealthChecksService(Configuration);

            services.AddFeatureManagement();

            services.AddGraphQLService(Configuration);

            services.AddAutoMapper(typeof(MappingProfile));

            services.AddDatabaseServicesExtension(Configuration);

            services.AddControllers()
                .AddFluentValidation()
                .AddJsonOptions(options => 
                    options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter()));

            services.AddCustomValidators();

            services.ConfigureCustomApiBehavior();

            services.AddSpaStaticFiles(configuration =>
            {
                configuration.RootPath = "../TrainReservation.UI/dist";
            });

            services.AddSwaggerGenService();            

            services.AddServicesExtension();            
        }
       
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Train reservations");
            });           

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");                
                app.UseHsts();
            }

            app.UseMiddleware<CustomExceptionMiddleware>();

            app.UseHttpsRedirection();

            app.UseStaticFiles();

            if (!env.IsDevelopment())
            {
                app.UseSpaStaticFiles();
            }

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapHealthEndpoint();

                endpoints.MapGraphQLEndpoint(Configuration);

                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller}/{action=Index}/{id?}");                
            });

            app.UseSpa(spa =>
            {
                spa.Options.SourcePath = "../TrainReservation.UI";

                if (env.IsDevelopment())
                {
                    spa.UseAngularCliServer(npmScript: "start");
                }
            });
        }
    }
}
