namespace Stockexchange.Service.StockExchangeRepository.Interface
{
    public interface IQueueService
    {
        Task SendMessage(string queueName, string message, string storageaccount);
    }
}
