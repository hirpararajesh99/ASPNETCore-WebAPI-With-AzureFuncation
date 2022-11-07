using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Stockexchange.Model.RequestModel;
using Stockexchange.Service.StockExchangeRepository.Interface;
using System;
using System.Threading.Tasks;

namespace StockExchangeFunctions
{
    public class StockQueuetrigger
    {
        private readonly IStockDetailRepository _stockDetailRepository;
        public StockQueuetrigger(IStockDetailRepository stockDetailRepository)
        {
            _stockDetailRepository = stockDetailRepository;
        }

        [FunctionName("StockQueuetrigger")]
        public async Task Run([QueueTrigger("stexchangequeue")] string myQueueItem, ILogger log)
        {
            log.LogInformation($"C# Queue trigger function processed: {myQueueItem}");

            try
            {
                var data = JsonConvert.DeserializeObject<StockTransactionRequestModel>(myQueueItem);

                await _stockDetailRepository.UpdateAsync(data);
            }
            catch (Exception ex)
            {
                log.LogError(ex.Message);
            }
        }
    }
}
