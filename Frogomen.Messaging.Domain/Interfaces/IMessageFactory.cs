using Frogomen.Messaging.Domain.Common;
using Frogomen.Messaging.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fragomen.Messaging.Domain.Interfaces
{
    public interface IMessageFactory
    {
        MessageBase CreateMessage(SendMessageRequest sendMessageRequest);
    }
}
