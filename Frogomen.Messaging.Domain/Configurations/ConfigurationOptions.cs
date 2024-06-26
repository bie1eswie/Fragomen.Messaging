

namespace Frogomen.Messaging.Domain.Configurations
{
    public class ConfigurationOptions
    {
        public SQSConfiguration SQS {  get; set; }
        public S3Configuration S3 { get; set; }
        public SendGridConfiguration SendGrid { get; set; }

        public ConfigurationOptions()
        {
            SQS = new SQSConfiguration();
            S3 = new S3Configuration();
            SendGrid = new SendGridConfiguration();
        }
    }
}
