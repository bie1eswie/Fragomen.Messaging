using Fragomen.Messaging.Application.UseCases.Get_Notification_By_Recipient;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fragomen.Messaging.Application.UseCases.Get_Message
{
    public class GetNotificationRequest: IRequest<GetNotificationResponse>
    {
        public string MessageRequestId {  get; set; }
        public bool IsCustomId {  get; set; }
    }
}
