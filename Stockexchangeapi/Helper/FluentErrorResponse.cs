using Newtonsoft.Json.Linq;

namespace Stockexchangeapi.Helper
{
    /// <summary>
    /// FluentErrorResponse
    /// </summary>
    public class FluentErrorResponse
    {
        /// <summary>
        /// 
        /// </summary>
        public int Code { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int Status { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public bool isFluentError { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public JObject Errors { get; set; }
    }
}
