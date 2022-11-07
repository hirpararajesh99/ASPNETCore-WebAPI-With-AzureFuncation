using Azure.Storage.Queues;
using Stockexchange.Service.StockExchangeRepository.Interface;

namespace Stockexchange.Service.StockExchangeRepository.Implementation
{
    public class QueueService : IQueueService
    {

        public async Task SendMessage(string queueName, string message, string storageaccount)
        {
            string connectionString = storageaccount;
            var queueClient = new QueueClient(connectionString, queueName, new QueueClientOptions { MessageEncoding = QueueMessageEncoding.Base64 });
            queueClient.CreateIfNotExists();
            if (queueClient.Exists())
            {
                await queueClient.SendMessageAsync(message);
            }
        }
    }
}
