using Amazon.DynamoDBv2;
using Amazon.SQS;
using Fragomen.Messaging.Application.Common.Interfaces;
using Fragomen.Messaging.Domain.Interfaces;
using Fragomen.Messaging.Infrastructure.Data;
using Fragomen.Messaging.Infrastructure.Queue;
using Fragomen.Messaging.Infrastructure.Services;
using Fragomen.Messaging.Infrastructure.Strategies;
using Frogomen.Messaging.Domain.Entities;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SendGrid.Helpers.Mail;

namespace Fragomen.Messaging.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<IMessageFactory, MessageFactory>();
            services.AddScoped<IMessageSenderService, MessageSenderService>();
            services.AddScoped<ISQSRepository, SQSRepository>();
            services.AddAWSService<IAmazonSQS>();
            services.AddAWSService<IAmazonDynamoDB>();
            services.AddScoped<IRepository<EmailMessage>, EmailMessageRepository>();
            return services;
        }   
    }
}
