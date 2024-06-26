using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.DocumentModel;
using Amazon.DynamoDBv2.Model;
using Fragomen.Messaging.Application.Common.Interfaces;
using Frogomen.Messaging.Domain.Common;
using Frogomen.Messaging.Domain.Entities;
using System.Net;
using System.Text.Json;

namespace Fragomen.Messaging.Infrastructure.Data
{
    public class EmailMessageRepository : IRepository<EmailMessage>
    {
        private readonly IAmazonDynamoDB _dynamoDB;

        public EmailMessageRepository(IAmazonDynamoDB dynamoDB)
        {
            _dynamoDB = dynamoDB;
        }
        public async Task<bool> CreateAsync(EmailMessage item)
        {
            var itemAsJson = JsonSerializer.Serialize(item);
            var itemAsAttributes = Document.FromJson(itemAsJson).ToAttributeMap();

            var createItemRequest = new PutItemRequest
            {
                TableName = Constants.EMAIL_MESSAGE_TABLE,
                Item = itemAsAttributes
            };
            var response = await _dynamoDB.PutItemAsync(createItemRequest);
            return response.HttpStatusCode == HttpStatusCode.OK;
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            var deleteItemRequest = new DeleteItemRequest
            {
                TableName = Constants.EMAIL_MESSAGE_TABLE,
                Key = new Dictionary<string, AttributeValue>
            {
                { "pk", new AttributeValue { S = id.ToString() } },
                { "sk", new AttributeValue { S = id.ToString() } }
            }
            };
            var response = await _dynamoDB.DeleteItemAsync(deleteItemRequest);
            return response.HttpStatusCode == HttpStatusCode.OK;
        }

        public async Task<IEnumerable<EmailMessage?>> GetAllAsync()
        {
            var scanRequest = new ScanRequest
            {
                TableName = Constants.NOTIFICATION_MESSAGE_TABLE,
            };

            var response = await _dynamoDB.ScanAsync(scanRequest);

            return response.Items.Select(x =>
            {
                var json = Document.FromAttributeMap(x).ToJson();
                return JsonSerializer.Deserialize<EmailMessage>(json);
            });
        }

        public async Task<EmailMessage?> GetByIdAsync(Guid id)
        {
            var getItemRequest = new GetItemRequest
            {
                TableName = Constants.EMAIL_MESSAGE_TABLE,
                Key = new Dictionary<string, AttributeValue>
            {
                { "pk", new AttributeValue { S = id.ToString() } },
                { "sk", new AttributeValue { S = id.ToString() } }
            }
            };
            var response = await _dynamoDB.GetItemAsync(getItemRequest);
            if (response.Item.Count == 0)
            {
                return default;
            }
            var itemAsDocument = Document.FromAttributeMap(response.Item);
            return JsonSerializer.Deserialize<EmailMessage>(itemAsDocument.ToJson());
        }

        public async Task<bool> UpdateAsync(EmailMessage item)
        {
            var itemAsJson = JsonSerializer.Serialize(item);
            var itemAsAttributes = Document.FromJson(itemAsJson).ToAttributeMap();

            var updateItemRequest = new PutItemRequest
            {
                TableName = Constants.EMAIL_MESSAGE_TABLE,
                Item = itemAsAttributes
            };
            var response = await _dynamoDB.PutItemAsync(updateItemRequest);
            return response.HttpStatusCode == HttpStatusCode.OK;
        }
    }
}
