using MediatR;
using Microsoft.EntityFrameworkCore;
using OpenIddictExample.IdP.Domain;
using System.Threading;
using System.Threading.Tasks;

namespace OpenIddictExample.IdP.Infrastructure.Queries
{
    public class GetUserByLoginData : IRequest<IUser<int>?>
    {
        public string Login { get; set; }

        public GetUserByLoginData() { }
    }

    public class GetUserByLoginDataRequestHandler : IRequestHandler<GetUserByLoginData, IUser<int>?>
    {
        private readonly OpenIddictExampleDbContext<int> context;

        public GetUserByLoginDataRequestHandler(OpenIddictExampleDbContext<int> context)
        {
            this.context = context;
        }

        public async Task<IUser<int>?> Handle(GetUserByLoginData request, CancellationToken cancellationToken)
        {
            return await context.Users.AsNoTracking().FirstOrDefaultAsync(x => x.Email.Equals(request.Login) || x.BoUserLogin.Equals(request.Login));
        }
    }
}
