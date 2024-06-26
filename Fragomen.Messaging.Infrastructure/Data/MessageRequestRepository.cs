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
    public class MessageRequestRepository : IRepository<SendMessageRequest>
    {
        private readonly IAmazonDynamoDB _dynamoDB;

        public MessageRequestRepository(IAmazonDynamoDB dynamoDB)
        {
            _dynamoDB = dynamoDB;
        }
        public async Task<bool> CreateAsync(SendMessageRequest item)
        {
            var itemAsJson = JsonSerializer.Serialize(item);
            var itemAsAttributes = Document.FromJson(itemAsJson).ToAttributeMap();

            var createItemRequest = new PutItemRequest
            {
                TableName = Constants.MESSAGE_REQUEST_TABLE,
                Item = itemAsAttributes
            };
            var response = await _dynamoDB.PutItemAsync(createItemRequest);
            return response.HttpStatusCode == HttpStatusCode.OK;
        }

        public Task<bool> DeleteAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<SendMessageRequest?>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<SendMessageRequest?> GetByIdAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<bool> UpdateAsync(SendMessageRequest item)
        {
            throw new NotImplementedException();
        }
    }
}
