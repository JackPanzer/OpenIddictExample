using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using OpenIddictExample.IdP.Domain;
using OpenIddictExample.IdP.Infrastructure.Queries;
using OpenIddictExample.IdP.ViewModels;
using System;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace OpenIddictExample.IdP.Commands.AuthenticateUser
{
    public class AuthenticateUserCommand : IRequest<UserAuthenticationResult>
    {
        public LoginViewModel Login { get; set; }
    }

    public class AuthenticateUserCommandHandler : IRequestHandler<AuthenticateUserCommand, UserAuthenticationResult>
    {
        private readonly IMediator mediator;
        private readonly IConfiguration config;
        private readonly ILogger log;

        public AuthenticateUserCommandHandler(IMediator mediator, IConfiguration config, ILogger<AuthenticateUserCommandHandler> log) 
        {
            this.mediator = mediator;
            this.config = config;
            this.log = log;
        }

        public async Task<UserAuthenticationResult> Handle(AuthenticateUserCommand request, CancellationToken cancellationToken)
        {
            var user = await this.mediator.Send(new GetUserByLoginData { Login = request.Login.User });

            if (user == null) return UserAuthenticationResult.NotFound;
            if (!user.Enabled) return UserAuthenticationResult.Disabled;
            if (user.Deleted) return UserAuthenticationResult.Deleted;
            if (!this.CheckPassword(user, request.Login.Password)) return UserAuthenticationResult.WrongPassword;

            return UserAuthenticationResult.Success;
        }

        private bool CheckPassword(IUser<int> user, string password)
        {
            try
            {
                using (var md5 = System.Security.Cryptography.MD5.Create())
                {
                    // In the example MD5 has been used for simplicity, in a real environment this wouldn't be pulled AT ALL
                    var computedPassword = md5.ComputeHash(Encoding.UTF8.GetBytes(password));
                    var storedPassword = Encoding.UTF8.GetBytes(user.Password);

                    return computedPassword.SequenceEqual(storedPassword);
                }
            }
            catch (Exception ex)
            {
                log.LogError(ex, "Error validating password");
                return false;
            }
        }
    }
}
