using Fragomen.Messaging.Application.UseCases.Get_Messages_by_Recipient;
using Fragomen.Messaging.Application.UseCases.Get_Notification_By_Recipient;
using Frogomen.Messaging.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fragomen.Messaging.Application.UseCases.Get_Notification_by_Recipient
{
    public class GetNotificationByRecipientRequestHandler : IRequestHandler<GetNotificationByRecipientRequest, GetNotificationByRecipientResponse>
    {
        public Task<GetNotificationByRecipientResponse> Handle(GetNotificationByRecipientRequest request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
