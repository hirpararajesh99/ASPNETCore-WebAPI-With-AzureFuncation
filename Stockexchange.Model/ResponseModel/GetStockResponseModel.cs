using Newtonsoft.Json;

namespace Stockexchange.Model.ResponseModel
{
    /// <summary>
    ///  GetStock ResponseModel
    /// </summary>
    public class GetStockResponseModel
    {
        /// <summary>
        /// The string that assign as Price
        /// </summary>
        [JsonProperty("price")]
        public decimal? Price { get; set; }


    }
}
