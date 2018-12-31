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
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.EntityFrameworkCore;
using BookStore.Api.Data;
using BookStore.Api.Repository;
using BookStore.Api.Services;
using BookStore.Api.Converters;

namespace BookStore.Api
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
            services.AddMemoryCache();

            services.AddMvcCore().AddAuthorization()
                  .AddJsonFormatters();

            var authenticationUrl = Configuration.GetValue<string>("IdentityServerAuthenticationUrl");
            services.AddAuthentication("Bearer")
                .AddIdentityServerAuthentication(options => {
                    options.Authority = authenticationUrl;
                    options.RequireHttpsMetadata = false;
                    options.ApiName = "api1";
                });

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

            var connection = Configuration.GetValue<string>("ConnectionString");
            services.AddDbContext<BookStoreContext>(options => options.UseSqlServer(connection));

            services.AddScoped<IBookStoreRepository, BookStoreRepository>();
            services.AddScoped<IBookStoreService, BookStoreService>();
            services.AddScoped<IBookStoreConverter, BookStoreConverter>();
            
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment()) {
                app.UseDeveloperExceptionPage();
            } else {
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseAuthentication();
            app.UseHttpsRedirection();
            app.UseMvc();
        }
    }
}
