
using Stockexchange.Model.RequestModel;
using Stockexchange.Model.ResponseModel;

namespace Stockexchange.Service.StockExchangeRepository.Interface
{
    public interface IStockDetailRepository
    {
        Task<GetStockResponseModel> Get(string ticker_symbol);
        Task SaveData(StockTransactionRequestModel stockTransactionRequestModel);
        Task UpdateAsync(StockTransactionRequestModel stockTransactionRequestModel);
    }
}
