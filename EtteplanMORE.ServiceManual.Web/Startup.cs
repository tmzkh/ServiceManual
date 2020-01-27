using EtteplanMORE.ServiceManual.ApplicationCore.AppContext;
using EtteplanMORE.ServiceManual.ApplicationCore.Interfaces;
using EtteplanMORE.ServiceManual.ApplicationCore.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace EtteplanMORE.ServiceManual.Web
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services){

            // add application db context to DI
            services.AddDbContext<FactoryContext>(opts => opts.UseMySql(Configuration.GetConnectionString("MySqlConnection"),
                    x => x.MigrationsAssembly("EtteplanMORE.ServiceManual.ApplicationCore")
                ));

            services.AddMvc();

            // add services to DI
            services.AddScoped<IFactoryDeviceService, FactoryDeviceService>();
            services.AddScoped<IMaintenanceTaskService, MaintenanceTaskService>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment()) {
                app.UseDeveloperExceptionPage();
            }
            app.UseMvc();
        }
    }
}