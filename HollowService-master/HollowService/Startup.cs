using HollowService.Interfaces;
using HollowService.Model;
using HollowService.Repository;
using HollowService.Service;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;

namespace HollowService
{
    public class Startup
    {
        string swaggerDocVersion = "v1";
        string swaggerDocTitle = "Hollow Service Api";
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<MyDBContext>();
            services.AddScoped<IEmployeeRepository, EmployeeRepository>();
            services.AddScoped<IEmployeeService, EmployeeService>();

            services.AddControllers();

            services.AddLogging(logging =>
            {
                logging.AddAWSProvider(Configuration.GetAWSLoggingConfigSection());
                logging.SetMinimumLevel(LogLevel.Debug);

            });

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc(swaggerDocVersion, new OpenApiInfo { Title = swaggerDocTitle, Version = swaggerDocVersion });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseSwagger();

            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", swaggerDocTitle);
            });

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
