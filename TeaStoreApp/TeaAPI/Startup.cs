using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;
using TeaDB;
using TeaDB.Entities;
using TeaLib;
using TeaDB.IRepo;
using TeaDB.IMappers;

namespace TeaAPI
{
    public class Startup
    {
        readonly string MyAllowSpecificOrigins = "_myAllowSpecificOrigins";
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCors(options => {
                options.AddPolicy(name: MyAllowSpecificOrigins,
                    builder => {
                        builder.WithOrigins("*")
                        .AllowAnyMethod()
                        .AllowAnyHeader();
                    });
            });

            services.AddControllers().AddXmlSerializerFormatters();
            services.AddDbContext<TeaContext>(options => options.UseNpgsql(Configuration.GetConnectionString("TeaDB")));
            
            services.AddScoped<MainMenuService>();
            services.AddScoped<IMainMenuRepo, DBRepo>();

            services.AddScoped<BasketService>();
            services.AddScoped<IBasketRepo, DBRepo>();

            services.AddScoped<ManagerService>();
            services.AddScoped<IManagerRepo, DBRepo>();

            services.AddScoped<LocationService>();
            services.AddScoped<ILocationRepo, DBRepo>();

            services.AddScoped<IMapper, DBMapper>();

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseCors(MyAllowSpecificOrigins);

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
