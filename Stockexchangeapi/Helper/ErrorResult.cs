using Newtonsoft.Json;

namespace Stockexchangeapi.Helper
{
    /// <summary>
    ///  ErrorResult
    /// </summary>
    public class ErrorResult
    {
        /// <summary>
        /// 
        /// </summary>
        [JsonProperty(PropertyName = "code")]
        public int Code { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [JsonProperty(PropertyName = "message")]
        public string Message { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [JsonProperty(PropertyName = "more_info")]
        public string MoreInfo { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [JsonProperty(PropertyName = "status")]
        public int Status { get; set; }
    }
}
