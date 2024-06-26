using Fragomen.Infrastructure;
using Frogomen.Messaging.Domain.Entities;
using Frogomen.Messaging.Domain.Exceptions;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Fragomen.Messaging.Controllers
{
    public class MessageController : ApiControllerBase
    {
        private readonly ILogger<MessageController> _logger;
        public MessageController(ILogger<MessageController> logger)
        {
            _logger = logger;
        }

        [HttpPut]
        public async Task<IActionResult> MessageRequest(SendMessageRequest sendMessageRequest)
        {
            var res  = await Mediator.Send(sendMessageRequest);
            return Ok(res);
        }
    }
}
