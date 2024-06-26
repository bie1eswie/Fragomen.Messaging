using Frogomen.Messaging.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace Frogomen.Messaging.Domain.Entities
{
    public class EmailMessage: MessageBase
    {
        public string To {  get; set; } = string.Empty;
        public string From { get; set; } = string.Empty;
        public string Subject { get; set; } = string.Empty;
        public string Text { get; set; } = string.Empty;
        public string Html { get; set; } = string.Empty;
        public EmailMessage() : base() 
        { 
        }
    }
}
