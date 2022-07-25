using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;

namespace OpenIddictExample.SP.Application.Controllers
{
    public class AccountController : Controller
    {
        private IConfiguration configuration;

        public AccountController(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        public IActionResult Login()
        {
            return Challenge(
                authenticationSchemes: "oidc",
                properties: new Microsoft.AspNetCore.Authentication.AuthenticationProperties
                {
                    RedirectUri = $"{this.configuration["oidc:callbackPath"]}"
                });
        }

        public IActionResult OAuthCallback()
        {
            throw new NotImplementedException();
        }
    }
}
