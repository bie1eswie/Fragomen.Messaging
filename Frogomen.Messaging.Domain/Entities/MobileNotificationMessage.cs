using Frogomen.Messaging.Domain.Common;
using Frogomen.Messaging.Domain.Enums;

namespace Frogomen.Messaging.Domain.Entities
{
    public class MobileNotificationMessage: MessageBase
    {
        public string Destination {  get; set; } = string.Empty;
        public string Message { get; set; } = string.Empty;
        public NotificationTypeEnum NotificationType { get; set; }
        public MobileNotificationMessage() 
        {

        }
    }
}
