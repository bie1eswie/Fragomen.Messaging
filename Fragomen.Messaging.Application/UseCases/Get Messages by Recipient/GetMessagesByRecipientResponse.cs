using Frogomen.Messaging.Domain.Common;

namespace Fragomen.Messaging.Application.UseCases.Get_Messages_by_Recipient
{
    public class GetMessagesByRecipientResponse
    {
        public IReadOnlyList<MessageBase> Messages { get; set; } = new List<MessageBase>();
    }
}
