using E_commerce.Data;
using E_commerce.Models.Repositories;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace E_commerce
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
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
            services.AddDbContext<APDbContext>(options =>
    options.UseMySql(
        Configuration.GetConnectionString("APDBConnection")
    )
);
            services.AddDbContext<SADbContext>(options =>
                options.UseMySql(
                    Configuration.GetConnectionString("SADBConnection")
                )
            );

            //session configs
            services.AddDistributedMemoryCache();
            services.AddSession(options => {
                options.IdleTimeout = TimeSpan.FromMinutes(240);//You can set Time (4hrs)   
            });
            
            services.AddScoped<IItemRepository, SQLItemRepository>();
            services.AddScoped<IUserRepository, SQLUserRepository>();
            services.AddScoped<IAccountRepository, SQLAccountRepository>();
            services.AddScoped<IPromotedItemRepository, SQLPromotedItemRepository>();
            services.AddScoped<IPurchasedItemRepository, SQLPurchasedItemRepository>();
            services.AddScoped<ITransactionHistoryRepository, SQLTransactionHistoryRepository>();
            services.AddScoped<ICartRepository, SQLCartRepository>();
            services.AddScoped<ITypeRepository, SQLTypeRepository>();
            services.AddControllers();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();
            app.UseSession(); //for session configs

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
