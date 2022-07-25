using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using OpenIddictExample.IdP.Infrastructure;
using OpenIddict.Abstractions;
using System;
using System.Threading;
using System.Threading.Tasks;
using static OpenIddict.Abstractions.OpenIddictConstants;

namespace OpenIddictExample.IdP.Application
{
    public class Worker : IHostedService
    {
        private readonly IServiceProvider _serviceProvider;

        public Worker(IServiceProvider serviceProvider)
            => _serviceProvider = serviceProvider;

        public async Task StartAsync(CancellationToken cancellationToken)
        {
            using var scope = _serviceProvider.CreateScope();

            var context = scope.ServiceProvider.GetRequiredService<OpenIddictExampleDbContext<int>>();
            await context.Database.EnsureCreatedAsync();

            var manager = scope.ServiceProvider.GetRequiredService<IOpenIddictApplicationManager>();

            var OpenIddictExampleClientCredentialsFlow = await manager.FindByClientIdAsync("OpenIddictExample");
            if (OpenIddictExampleClientCredentialsFlow is not null)
            {
                await manager.DeleteAsync(OpenIddictExampleClientCredentialsFlow);
            }

            await manager.CreateAsync(new OpenIddictApplicationDescriptor
            {
                ClientId = "OpenIddictExample",
                ClientSecret = "388D45FA-B36B-4988-BA59-B187D329C207",
                DisplayName = "OpenIddictExample LaLey",
                RedirectUris = { new Uri("https://localhost:44392/Account/OAuthCallback") },
                Permissions =
                {
                    OpenIddictConstants.Permissions.Endpoints.Authorization,
                    OpenIddictConstants.Permissions.Endpoints.Token,

                    OpenIddictConstants.Permissions.GrantTypes.AuthorizationCode,
                    OpenIddictConstants.Permissions.GrantTypes.ClientCredentials,

                    $"{ OpenIddictConstants.Permissions.Prefixes.Scope }api",
                    OpenIddictConstants.Permissions.Scopes.Profile,

                    OpenIddictConstants.Permissions.ResponseTypes.Code
                }
            });

            var postmanAuthFlow = await manager.FindByClientIdAsync("postman");
            if (postmanAuthFlow is not null)
            {
                await manager.DeleteAsync(postmanAuthFlow);
            }

            await manager.CreateAsync(new OpenIddictApplicationDescriptor
            {
                ClientId = "postman",
                ClientSecret = "postman-secret",
                DisplayName = "Postman",
                RedirectUris = { new Uri("https://oauth.pstmn.io/v1/callback") },
                Permissions =
                {
                    OpenIddictConstants.Permissions.Endpoints.Authorization,
                    OpenIddictConstants.Permissions.Endpoints.Token,

                    OpenIddictConstants.Permissions.GrantTypes.AuthorizationCode,
                    OpenIddictConstants.Permissions.GrantTypes.ClientCredentials,

                    $"{ OpenIddictConstants.Permissions.Prefixes.Scope }api",
                    OpenIddictConstants.Permissions.Scopes.Profile,

                    OpenIddictConstants.Permissions.ResponseTypes.Code
                }

            });
        }

        public Task StopAsync(CancellationToken cancellationToken) => Task.CompletedTask;
    }
}
