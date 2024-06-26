using Frogomen.Messaging.Domain.Common;

namespace Fragomen.Messaging.Application.UseCases.Get_Notification_By_Recipient
{
    public class GetNotificationByRecipientResponse
    {
        public IReadOnlyList<MessageBase> Messages { get; set; } = new List<MessageBase>();
    }
}
