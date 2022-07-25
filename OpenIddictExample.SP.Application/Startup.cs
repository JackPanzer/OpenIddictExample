using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace OpenIddictExample.SP.Application
{
    public class Startup
    {
        private IConfiguration configuration;

        public Startup(IConfiguration configuration) 
        {
            this.configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllersWithViews();

            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                .AddCookie(CookieAuthenticationDefaults.AuthenticationScheme, options => { options.LoginPath = "/Account/Login"; })
                .AddOpenIdConnect("oidc", options =>
                {
                    options.RequireHttpsMetadata = false;
                    options.SaveTokens = true;

                    options.Authority = this.configuration["oidc:authority"];
                    options.ClientId = this.configuration["oidc:clientId"];
                    options.ClientSecret = this.configuration["oidc:clientSecret"];
                    options.ResponseType = this.configuration["oidc:responseType"];
                    options.CallbackPath = $"{this.configuration["oidc:callbackPath"]}";
                });
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints => 
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}"
                    );
            });
        }
    }
}
