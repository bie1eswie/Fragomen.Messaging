using Frogomen.Messaging.Domain.Enums;
using MediatR;

namespace Frogomen.Messaging.Domain.Entities
{
    public class SendMessageRequest: IRequest<SendMessageResponse>
    {
        public Dictionary<string, string> MessageOptions { get; set; } = new Dictionary<string, string>();
        public Dictionary<string, string> MessageData { get;set; } = new Dictionary<string, string>();  
        public MessageTypeEnum MessageType { get; set; }
        public IReadOnlyList<Recipient> Recipients { get; set; }  = new List<Recipient> { };

    }
}
