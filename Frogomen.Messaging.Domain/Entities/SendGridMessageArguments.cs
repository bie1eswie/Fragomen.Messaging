using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Frogomen.Messaging.Domain.Entities
{
    public class SendGridMessageArguments
    {
        public string RequestId {  get; set; } = string.Empty;
        public string GlobalFrom { get; set; } = string.Empty;
        public string GlobalReplyTo { get; set; } = string.Empty;
        public string TemplateId {  get; set; } = string.Empty;
        public string BatchId { get; set; } = string.Empty;
    }
}
