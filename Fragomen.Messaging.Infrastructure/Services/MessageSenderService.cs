using Fragomen.Messaging.Application.Common.Interfaces;
using Fragomen.Messaging.Domain.Interfaces;
using Frogomen.Messaging.Domain.Configurations;
using Frogomen.Messaging.Domain.Entities;
using Microsoft.Extensions.Options;

namespace Fragomen.Messaging.Infrastructure.Services
{
    public class MessageSenderService : IMessageSenderService
    {
        private readonly ISQSRepository _sQSRepository;
        private readonly IMessageFactory _messageFactory;
        public MessageSenderService(ISQSRepository sQSRepository, IOptions<ConfigurationOptions> options, IMessageFactory messageFactory)
        {
            _sQSRepository = sQSRepository;
            _messageFactory = messageFactory;
        }
        public async Task<SendMessageResponse> SendMessage(SendMessageRequest messageRequest)
        {
            var message = _messageFactory.CreateMessage(messageRequest);
            message = await _sQSRepository.Enqueue(message);
            return new SendMessageResponse()
            {
                Success = true,
                MessageId = message.Id.ToString(),
            };
        }
    }
}
