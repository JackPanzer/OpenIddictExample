using MediatR;
using Microsoft.EntityFrameworkCore;
using OpenIddictExample.IdP.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace OpenIddictExample.IdP.Infrastructure.Queries
{
    public class GetUserByIdQuery : IRequest<IUser<int>?>
    {
        public int Id { get; set; }

        public GetUserByIdQuery() { }
    }

    public class GetUserByIdRequestHandler : IRequestHandler<GetUserByIdQuery, IUser<int>?>
    {
        private readonly OpenIddictExampleDbContext<int> context;

        public GetUserByIdRequestHandler(OpenIddictExampleDbContext<int> context)
        {
            this.context = context;
        }

        public async Task<IUser<int>?> Handle(GetUserByIdQuery request, CancellationToken cancellationToken)
        {
            var user = await context.Users.AsNoTracking().FirstOrDefaultAsync(x => x.Id == request.Id);

            return user;
        }
    }
}
