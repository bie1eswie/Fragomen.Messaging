using Amazon.Runtime.Internal.Endpoints.StandardLibrary;
using Amazon.S3;
using Amazon.SQS;
using Amazon.SQS.Model;
using Fragomen.Messaging.Application.Common.Interfaces;
using Frogomen.Messaging.Domain.Common;
using Frogomen.Messaging.Domain.Configurations;
using Frogomen.Messaging.Domain.Entities;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;

namespace Fragomen.Messaging.Infrastructure.Queue
{
    public class SQSRepository : ISQSRepository
    {
        private readonly IAmazonSQS sqsClient;
        private readonly IOptions<ConfigurationOptions> _options;
        private readonly IRepository<EmailMessage> emailMessageRepository;

        public SQSRepository(IOptions<ConfigurationOptions> options, IRepository<EmailMessage> emailMessageRepository)
        {
            this.sqsClient = new AmazonSQSClient();
            this._options = options;
            this.emailMessageRepository = emailMessageRepository;
        }
        public async Task<MessageBase> Enqueue(MessageBase messageRequest)
        {
                string messageBody = JsonConvert.SerializeObject(messageRequest);
                var request = new Amazon.SQS.Model.SendMessageRequest()
                {
                    MessageGroupId = "emails",
                    MessageDeduplicationId = Guid.NewGuid().ToString(),
                    MessageBody = messageBody,
                    QueueUrl = _options.Value.SQS.MessageQueueUrl
                };
                var res = await sqsClient.SendMessageAsync(request);
            if (res.HttpStatusCode == System.Net.HttpStatusCode.OK)
            {
                messageRequest.CustomArgs.RequestId = res.MessageId;
                await emailMessageRepository.CreateAsync(messageRequest as EmailMessage);
            }
            return messageRequest;
        }
    }
}
