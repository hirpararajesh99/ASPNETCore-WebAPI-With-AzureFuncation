using fieldflake_coreflow.Services.GloContextRepository;
using Microsoft.AspNetCore.Http;
using Stockexchange.Model.RequestModel;
using Stockexchange.Model.ResponseModel;
using Stockexchange.Model.StockExchangeDB;
using Stockexchange.Service.StockExchangeRepository.Interface;

namespace Stockexchange.Service.StockExchangeRepository.Implementation
{
    public class StockDetailRepository : IStockDetailRepository
    {
        IUnitofWorkRepository _unitofWorkRepository;

        public StockDetailRepository(IUnitofWorkRepository unitofWorkRepository)
        {
            _unitofWorkRepository = unitofWorkRepository;
        }

        public async Task<GetStockResponseModel> Get(string ticker_symbol)
        {
            var stock = await _unitofWorkRepository.RepositoryAsync<StockDetail>().SingleOrDefaultAsync(x => x.TickerSymbol == ticker_symbol);
            if (stock == null)
            {
                throw new HttpStatusCodeException(StatusCodes.Status404NotFound, "No data Found");
            }
            var response = new GetStockResponseModel();

            response.Price = stock.CurrentPrice;

            return response;
        }

        public async Task SaveData(StockTransactionRequestModel stockTransactionRequestModel)
        {
            var Stocktransactiononj = new Transaction();
            Stocktransactiononj.TransactionType = stockTransactionRequestModel.TransactionType;
            Stocktransactiononj.TransactionPrice = stockTransactionRequestModel.TransactionPrice;
            Stocktransactiononj.TransactionDateTime = stockTransactionRequestModel.TransactionDateTime;
            Stocktransactiononj.UserId = stockTransactionRequestModel.Userid;

            await _unitofWorkRepository.RepositoryAsync<Transaction>().InsertAsync(Stocktransactiononj);
            await _unitofWorkRepository.CommitAsync();
        }

        public async Task UpdateAsync(StockTransactionRequestModel stockTransactionRequestModel)
        {
            var stock = await _unitofWorkRepository.RepositoryAsync<StockDetail>().SingleOrDefaultAsync(x => x.TickerSymbol == stockTransactionRequestModel.TickerSymbol);
            if (stock != null)
            {
                stock.CurrentPrice = stockTransactionRequestModel.TransactionPrice;

                _unitofWorkRepository.RepositoryAsync<StockDetail>().Update(stock);
                await _unitofWorkRepository.CommitAsync();
            }
        }
    }
}
