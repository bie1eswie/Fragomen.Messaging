using Frogomen.Messaging.Domain.Common;
using Frogomen.Messaging.Domain.Entities;

namespace Fragomen.Messaging.Application.Common.Interfaces
{
    public interface IMessageSenderService
    {
        Task<SendMessageResponse> SendMessage(SendMessageRequest message);
    }
}
