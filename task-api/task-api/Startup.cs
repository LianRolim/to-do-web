using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Mvc;
using task_api.Services;
using task_api.Services.Implementations;

namespace task_api
{
    public class Startup
    {

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        readonly string MyAllowSpecificOrigins = "_myAllowSpecificOrigins";

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services){

            //libero o CORS
            services.AddCors(options => { options.AddPolicy("CorsPolicy",
                                          builder => builder.AllowAnyOrigin()
                                                            .AllowAnyMethod()
                                                            .AllowAnyHeader()
                                                            .AllowCredentials());
            });
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

            //Injeto a dependencia do service
            services.AddScoped<ITaskService, TaskServiceImpl>();
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }else{
                app.UseHsts();
            }

            //proxy
            app.UseHttpsRedirection();
            app.UseCors("CorsPolicy"); //libera o cors
            app.UseMvc();
        }
    }
}
