using MediatR;

namespace Fragomen.Messaging.Application.UseCases.Get_Message
{
    public class GetNotificationRequestHandler : IRequestHandler<GetNotificationRequest, GetNotificationResponse>
    {
        public Task<GetNotificationResponse> Handle(GetNotificationRequest request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
