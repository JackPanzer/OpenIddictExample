using MediatR;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using OpenIddictExample.IdP.Infrastructure;
using System.Reflection;
using static OpenIddict.Abstractions.OpenIddictConstants;

namespace OpenIddictExample.IdP.Application
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
            services.AddMvc();

            var currentAssembly = new[]
            {
                Assembly.Load("OpenIddictExample.IdP.Application"),
                Assembly.Load("OpenIddictExample.IdP.Infrastructure")
            };

            services.AddMediatR(currentAssembly);

            var connectionString = Configuration.GetConnectionString("OpenIddictExample");

            services.AddDbContext<OpenIddictExampleDbContext<int>>(options =>
            {
                options.UseSqlServer(connectionString, b => b.MigrationsAssembly("OpenIddictExample.IdP.Application"));
                options.UseOpenIddict<int>();
            });

            services.AddCors(options =>
            {
                options.AddPolicy("AllowAllOrigins",
                    builder =>
                    {
                        builder
                            .AllowCredentials()
                            .WithOrigins(
                                $"{Configuration.GetSection("CORS:WhitelistedDomains").Get<string[]>() }")
                            .SetIsOriginAllowedToAllowWildcardSubdomains()
                            .AllowAnyHeader()
                            .AllowAnyMethod();
                    });
            });

            services.AddOpenIddict()
                .AddCore(options =>
                {
                    options.UseEntityFrameworkCore()
                          .UseDbContext<OpenIddictExampleDbContext<int>>()
                          .ReplaceDefaultEntities<int>();
                })
                .AddServer(options =>
                {
                    // Enable the authorization and token endpoints.
                    options
                        .SetAuthorizationEndpointUris("/connect/authorize")
                        .SetTokenEndpointUris("/connect/token")
                        .SetUserinfoEndpointUris("/connect/userinfo");

                    // Enable the client credentials flow.
                    //options.AllowClientCredentialsFlow();
                    options
                        .AllowAuthorizationCodeFlow()
                        .AllowRefreshTokenFlow();

                    // Mark the "email", "profile", "roles" and "dataEventRecords" scopes as supported scopes.
                    options.RegisterScopes(Scopes.Email, Scopes.Profile, Scopes.Roles, "dataEventRecords", "api");

                    // Register the signing and encryption credentials.
                    options.AddDevelopmentEncryptionCertificate()
                           .AddDevelopmentSigningCertificate()
                           .DisableAccessTokenEncryption();

                    // Force client applications to use Proof Key for Code Exchange (PKCE).
                    options.RequireProofKeyForCodeExchange();

                    // Register the ASP.NET Core host and configure the ASP.NET Core options.
                    options.UseAspNetCore()
                           .EnableStatusCodePagesIntegration()
                           .EnableAuthorizationEndpointPassthrough()
                           .EnableLogoutEndpointPassthrough()
                           .EnableTokenEndpointPassthrough()
                           .EnableUserinfoEndpointPassthrough()
                           .EnableVerificationEndpointPassthrough()
                           .DisableTransportSecurityRequirement(); // During development, you can disable the HTTPS requirement.

                })
                // Register the OpenIddict validation components.
                .AddValidation(options =>
                {
                    // Configure the audience accepted by this resource server.
                    // The value MUST match the audience associated with the
                    // "demo_api" scope, which is used by ResourceController.
                    // options.AddAudiences("rs_dataEventRecordsApi");

                    // Import the configuration from the local OpenIddict server instance.
                    options.UseLocalServer();

                    // Register the ASP.NET Core host.
                    options.UseAspNetCore();

                    // For applications that need immediate access token or authorization
                    // revocation, the database entry of the received tokens and their
                    // associated authorizations can be validated for each API call.
                    // Enabling these options may have a negative impact on performance.
                    //
                    // options.EnableAuthorizationEntryValidation();
                    // options.EnableTokenEntryValidation();
                });

            services.AddHostedService<Worker>();
            
            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                .AddCookie(CookieAuthenticationDefaults.AuthenticationScheme, options =>
                {
                    options.LoginPath = "/account/login";
                });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseCors("AllowAllOrigins");
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                //endpoints.MapControllers();
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}