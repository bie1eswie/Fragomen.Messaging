using Frogomen.Messaging.Domain.Common;
using Frogomen.Messaging.Domain.Entities;
using Frogomen.Messaging.Domain.Entities.S3;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fragomen.Messaging.Application.Common.Interfaces
{
    public interface ISQSRepository
    {
        Task<MessageBase> Enqueue(MessageBase messageRequest);
    }
}
