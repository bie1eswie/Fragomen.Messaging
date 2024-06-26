using Frogomen.Messaging.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Frogomen.Messaging.Domain.Entities
{
    public class Recipient
    {
        public string Address { get; set; } = string.Empty;
        public RecipientTypeEnum RecipientType { get; set; }
        public string Name { get; set; } = string.Empty;
    }
}
