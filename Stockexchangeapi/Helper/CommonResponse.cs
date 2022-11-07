using Newtonsoft.Json;

namespace Stockexchangeapi.Helper
{
    public class NotFoundResponse
    {
        /// <summary>
        /// (Conditional) An error code to find help for the exception.
        /// </summary>
        [JsonProperty("code")]

        public int code { get; set; }

        /// <summary>
        /// A more descriptive message regarding the exception.
        /// </summary>
        [JsonProperty("message")]
        public string message { get; set; }

        /// <summary>
        /// The HTTP status code for the exception.
        /// </summary>
        [JsonProperty("status")]
        public int status { get; set; }

    }

    public class BadRequestResponse
    {
        /// <summary>
        /// (Conditional) An error code to find help for the exception.
        /// </summary>
        [JsonProperty("code")]

        public int code { get; set; }
        /// <summary>
        /// A more descriptive message regarding the exception.
        /// </summary>
        [JsonProperty("message")]
        public string message { get; set; }

        /// <summary>
        ///  The HTTP status code for the exception.
        /// </summary>
        [JsonProperty("status")]
        public int status { get; set; }
    }

    public class InternalServerErrorReponse
    {
        /// <summary>
        /// (Conditional) An error code to find help for the exception.
        /// </summary>
        [JsonProperty("code")]

        public int code { get; set; }
        /// <summary>
        /// A more descriptive message regarding the exception.
        /// </summary>
        [JsonProperty("message")]
        public string message { get; set; }

        /// <summary>
        /// The HTTP status code for the exception.
        /// </summary>
        [JsonProperty("status")]
        public int status { get; set; }
    }

    public class UnauthorizedResponse
    {
        /// <summary>
        /// (Conditional) An error code to find help for the exception.
        /// </summary>
        [JsonProperty("code")]

        public int code { get; set; }

        /// <summary>
        /// A more descriptive message regarding the exception.
        /// </summary>
        [JsonProperty("message")]
        public string message { get; set; }

        /// <summary>
        /// The HTTP status code for the exception.
        /// </summary>
        [JsonProperty("status")]
        public int status { get; set; }

    }
}
