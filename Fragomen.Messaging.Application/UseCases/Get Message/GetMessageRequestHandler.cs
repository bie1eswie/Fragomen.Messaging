using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fragomen.Messaging.Application.UseCases.Get_Message
{
    public class GetMessageRequestHandler : IRequestHandler<GetMessageRequest, GetMessageResponse>
    {
        public Task<GetMessageResponse> Handle(GetMessageRequest request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
