using Newtonsoft.Json;

namespace Stockexchange.Model.RequestModel
{
    /// <summary>
    ///  GetStock ResponseModel
    /// </summary>
    public class StockTransactionRequestModel
    {

        /// <summary>
        /// The string that assign as Ticker Symbol
        /// </summary>
        [JsonProperty("ticker_symbol")]
        public string? TickerSymbol { get; set; }

        /// <summary>
        /// The value that assign as Transaction DateTime 
        /// </summary>
        [JsonProperty("transaction_datetime")]
        public DateTime? TransactionDateTime { get; set; }

        /// <summary>
        /// The value that assign as User id
        /// </summary>
        [JsonProperty("user_id")]
        public long Userid { get; set; }

        /// <summary>
        /// The string that assign as Teansaction Type
        /// </summary>
        [JsonProperty("transaction_type")]
        public string? TransactionType { get; set; }

        /// <summary>
        /// The value that assign as Transaction Price
        /// </summary>
        [JsonProperty("transaction_price")]
        public decimal? TransactionPrice { get; set; }

    }
}
