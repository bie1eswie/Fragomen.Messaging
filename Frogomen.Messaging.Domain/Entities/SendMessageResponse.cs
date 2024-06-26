using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Frogomen.Messaging.Domain.Entities
{
    public class SendMessageResponse 
    {
        public bool Success { get; set; }
        public string MessageId {  get; set; } = string.Empty;
    }
}
