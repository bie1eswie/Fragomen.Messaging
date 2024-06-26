using Fragomen.Messaging.Domain.Interfaces;
using Frogomen.Messaging.Domain.Common;
using Frogomen.Messaging.Domain.Entities;
using Frogomen.Messaging.Domain.Enums;
using System.Reflection;

namespace Fragomen.Messaging.Infrastructure.Strategies
{
    public class MessageFactory : IMessageFactory
    {
        public MessageBase CreateMessage(SendMessageRequest sendMessageRequest)
        {
            MessageBase message;
            switch (sendMessageRequest.MessageType)
            {
                case MessageTypeEnum.Email:
                    message = new EmailMessage();
                    break;
                default:
                    return default;
            }

            var oldDictionary = sendMessageRequest.MessageData;
            var comparer = StringComparer.OrdinalIgnoreCase;
            var newDictionary = new Dictionary<string, string>(oldDictionary, comparer);

            var propertyInfos = message.GetType().GetProperties();
            foreach (PropertyInfo pInfo in propertyInfos)
            {
                string propertyName = pInfo.Name; //gets the name of the property
                string value = string.Empty;
                
                if (newDictionary.TryGetValue(propertyName.ToLower(),out value!))
                {
                    pInfo.SetValue(message, value, null);
                }
            }

            var customInfos = message.CustomArgs.GetType().GetProperties();
            foreach (PropertyInfo pInfo in customInfos)
            {
                string propertyName = pInfo.Name; //gets the name of the property
                string value = string.Empty;

                if (sendMessageRequest.MessageOptions.TryGetValue(propertyName.ToLower(), out value!))
                {
                    pInfo.SetValue(message, value, null);
                }
            }

            return message;

        }
    }
}
