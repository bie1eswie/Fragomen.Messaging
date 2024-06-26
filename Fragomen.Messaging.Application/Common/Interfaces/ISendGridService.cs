using Frogomen.Messaging.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fragomen.Messaging.Application.Common.Interfaces
{
    public interface ISendGridService
    {
        Task<T> GetMessage<T>(string messageId) where T : MessageBase, new();
        Task<IReadOnlyList<T>> GetMessages<T>(string recipient) where T : MessageBase, new();
    }
}
