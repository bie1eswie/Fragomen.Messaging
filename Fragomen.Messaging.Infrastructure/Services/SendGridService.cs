using Fragomen.Messaging.Application.Common.Interfaces;
using Frogomen.Messaging.Domain.Common;
using Frogomen.Messaging.Domain.Configurations;
using Frogomen.Messaging.Domain.Exceptions;
using Microsoft.Extensions.Options;
using SendGrid;
using System.Net.Http.Json;
using System.Text.Json;

namespace Fragomen.Messaging.Infrastructure.Services
{
    public class SendGridService : ISendGridService
    {
        private readonly SendGridClient _client;
        private readonly IOptions<ConfigurationOptions> _options;
        public SendGridService(IOptions<ConfigurationOptions> options)
        {
            _client = new SendGridClient(options.Value.SendGrid.APIKey);
            _options = options;
        }
        
        public async Task<T> GetMessage<T>(string messageId) where T : MessageBase, new()
        {
            var queryParams = JsonSerializer.Serialize(new
            {
                query = $"msg_id LIKE '{messageId}%'"
            });

            var response = await _client.RequestAsync(
                method: SendGridClient.Method.GET,
                urlPath: "messages",
                queryParams: queryParams
            );

            if (response.IsSuccessStatusCode)
            {
                var result = await response.Body.ReadFromJsonAsync<IReadOnlyList<T>>();
                return result?.FirstOrDefault() ?? new T();
            }
            else
            {
                throw new APIException((int)response.StatusCode, $"API request failed");
            }
        }

        public async Task<IReadOnlyList<T>> GetMessages<T>(string recipient) where T : MessageBase, new()
        {
            var queryParams = JsonSerializer.Serialize(new
            {
                query = $"to_email LIKE '{recipient}%'"
            });

            var response = await _client.RequestAsync(
                method: SendGridClient.Method.GET,
                urlPath: "messages",
                queryParams: queryParams
            );
            if (response.IsSuccessStatusCode)
            {
                var result = await response.Body.ReadFromJsonAsync<IReadOnlyList<T>>();
                return result?? new List<T>();
            }
            else
            {
                throw new APIException((int)response.StatusCode, $"API request failed");
            }
        }
    }
}
