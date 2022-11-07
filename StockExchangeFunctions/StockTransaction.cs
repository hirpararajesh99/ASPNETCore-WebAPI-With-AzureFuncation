using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Stockexchange.Model.RequestModel;
using Stockexchange.Service.StockExchangeRepository.Interface;
using System;
using System.IO;
using System.Threading.Tasks;

namespace StockExchangeFunctions
{
    public class StockTransaction
    {
        private readonly IStockDetailRepository _stockDetailRepository;
        private readonly IQueueService _queueService;

        public StockTransaction(IStockDetailRepository stockDetailRepository, IQueueService queueService)
        {
            _stockDetailRepository = stockDetailRepository;
            _queueService = queueService;
        }

        [FunctionName("StockTransaction")]
        public async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = "stock_transaction")] HttpRequest req,
            ILogger log)
        {
            log.LogInformation("C# HTTP trigger Transaction function processed a request.");
            var config = ConfigHelper.GetConfig();

            var storageaccount = config["StorageConnectionString"];
            try
            {
                string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
                var data = JsonConvert.DeserializeObject<StockTransactionRequestModel>(requestBody);
                await _stockDetailRepository.SaveData(data);
                await _queueService.SendMessage("stexchangequeue", JsonConvert.SerializeObject(data), storageaccount);
            }
            catch (Exception ex)
            {
                log.LogError(ex.Message.ToString());
            }
            return new OkResult();
        }
    }
}
