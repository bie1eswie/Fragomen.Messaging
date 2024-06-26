

namespace Frogomen.Messaging.Domain.Configurations
{
    public class SQSConfiguration
    {
        public string MessageQueueUrl {  get; set; } = string.Empty;
        public string ResendQueueUrl {  get; set; } = string.Empty;
    }
}
