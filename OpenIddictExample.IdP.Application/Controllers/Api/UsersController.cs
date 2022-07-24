using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OpenIddictExample.IdP.Domain;
using OpenIddictExample.IdP.Infrastructure.Queries;
using System;
using System.Threading.Tasks;

namespace OpenIddictExample.IdP.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IMediator mediator;

        public UsersController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        [HttpGet("{id}")]
        public async Task<IUser<int>?> GetUserById(int id)
        {
            var result = await mediator.Send(new GetUserByIdQuery { Id = id });
            return result;
        }
    }
}
