using Frogomen.Messaging.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fragomen.Messaging.Application.UseCases.Get_Message
{
    public class GetMessageResponse
    {
        public MessageBase Message { get; set; }
    }
}
