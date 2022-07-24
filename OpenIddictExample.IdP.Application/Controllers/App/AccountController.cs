using MediatR;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OpenIddictExample.IdP.Commands.AuthenticateUser;
using OpenIddictExample.IdP.Models;
using OpenIddictExample.IdP.ViewModels;
using OpenIddictExample.IdP.Infrastructure.Queries;
using OpenIddict.Server.AspNetCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace OpenIddictExample.IdP.Controllers
{
    [Route("/[controller]")]
    public class AccountController : Controller
    {
        private readonly IMediator mediator;

        public AccountController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        [HttpGet("login")]
        [AllowAnonymous]
        public IActionResult Login(string returnUrl = null)
        {
            if (User?.Identity?.IsAuthenticated ?? false)
            {
                RedirectToAction("Index", "Home");
            }

            ViewData["ReturnUrl"] = returnUrl;
            return View();
        }

        [HttpPost("login")]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            ViewData["ReturnUrl"] = model.ReturnUrl;

            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var validationStatus = await this.mediator.Send(new AuthenticateUserCommand { Login = model });

            if (validationStatus != UserAuthenticationResult.Success)
            {
                ViewData["DisplayError"] = true;
                return View(model);
            }

            var userModel = await this.mediator.Send(new GetUserByLoginData { Login = model.User });
            var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, userModel.Guid),
                    new Claim(OpenIddictExampleClaimTypes.UserEmail, userModel.Email),
                    new Claim(OpenIddictExampleClaimTypes.UserFormalName, $"{ userModel.Name } { userModel.LastName }")
                };

            var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

            await HttpContext.SignInAsync(new ClaimsPrincipal(claimsIdentity));

            if (Url.IsLocalUrl(model.ReturnUrl))
            {
                return Redirect(model.ReturnUrl);
            }

            return RedirectToAction(nameof(HomeController.Index), "Home");
        }

        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync();

            return RedirectToAction(nameof(HomeController.Index), "Home");
        }
    }
}
