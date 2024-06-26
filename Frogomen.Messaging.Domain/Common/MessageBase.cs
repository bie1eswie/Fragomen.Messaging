using Frogomen.Messaging.Domain.Entities;
using Frogomen.Messaging.Domain.Enums;
using System.Text.Json.Serialization;

namespace Frogomen.Messaging.Domain.Common
{
    public abstract class MessageBase
    {
        [JsonPropertyName("pk")]
        public string Pk => Id.ToString();

        [JsonPropertyName("sk")]
        public string SK => Id.ToString();
        public Guid Id { get; init; } = default!;
        public MessageTypeEnum MessageType { get; set; }
        public SendGridMessageArguments CustomArgs { get; set; }

        public MessageBase()
        {
            CustomArgs = new SendGridMessageArguments();
            Id = Guid.NewGuid();
        }
    }
}
