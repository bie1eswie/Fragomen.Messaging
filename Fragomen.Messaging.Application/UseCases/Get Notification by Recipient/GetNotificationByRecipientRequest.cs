using Fragomen.Messaging.Application.UseCases.Get_Notification_By_Recipient;
using MediatR;

namespace Fragomen.Messaging.Application.UseCases.Get_Messages_by_Recipient
{
    public class GetNotificationByRecipientRequest: IRequest<GetNotificationByRecipientResponse>
    {
        public Guid Id { get; set; }
    }
}
