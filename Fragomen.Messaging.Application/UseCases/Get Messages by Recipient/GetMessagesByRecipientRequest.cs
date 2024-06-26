using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fragomen.Messaging.Application.UseCases.Get_Messages_by_Recipient
{
    public class GetMessagesByRecipientRequest: IRequest<GetMessagesByRecipientResponse>
    {
        public Guid Id { get; set; }
    }
}
