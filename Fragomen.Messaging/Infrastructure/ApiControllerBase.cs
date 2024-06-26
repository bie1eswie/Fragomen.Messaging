using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Fragomen.Infrastructure
{
    [ApiController]
    [Route("api/[controller]")]
    public abstract class ApiControllerBase : ControllerBase
    {
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        private ISender _mediator;
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
#pragma warning disable CS8603 // Possible null reference return.
        protected ISender Mediator => _mediator ??= HttpContext.RequestServices.GetService<ISender>();
#pragma warning restore CS8603 // Possible null reference return.
    }
}
