using MediatR;

namespace Fragomen.Messaging.Application.UseCases.Get_Messages_by_Recipient
{
    public class GetMessagesByRecipientRequestHandler : IRequestHandler<GetMessagesByRecipientRequest, GetMessagesByRecipientResponse>
    {
        public Task<GetMessagesByRecipientResponse> Handle(GetMessagesByRecipientRequest request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
