using Fragomen.Messaging.Application.Common.Interfaces;
using Frogomen.Messaging.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fragomen.Messaging.Application.UseCases.SendMessage
{
    public class SendMessageRequestHandler : IRequestHandler<SendMessageRequest, SendMessageResponse>
    {
        private readonly IMessageSenderService _messageSenderService;
        public SendMessageRequestHandler(IMessageSenderService messageSenderService)
        {
            _messageSenderService = messageSenderService;
        }
        public async Task<SendMessageResponse> Handle(SendMessageRequest request, CancellationToken cancellationToken)
        {
            return await _messageSenderService.SendMessage(request);
        }
    }
}
