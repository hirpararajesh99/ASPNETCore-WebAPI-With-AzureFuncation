using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Serialization;
using Stockexchange.Service;

namespace Stockexchangeapi.Helper
{
    /// <summary>
    /// 
    /// </summary>
    public class HttpStatusCodeExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<HttpStatusCodeExceptionMiddleware> _logger;
        private object requestBody;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="next"></param>
        /// <param name="loggerFactory"></param>
        public HttpStatusCodeExceptionMiddleware(RequestDelegate next, ILoggerFactory loggerFactory)
        {
            _next = next ?? throw new ArgumentNullException(nameof(next));
            _logger = loggerFactory?.CreateLogger<HttpStatusCodeExceptionMiddleware>() ?? throw new ArgumentNullException(nameof(loggerFactory));
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (HttpStatusCodeException ex)
            {
                if (context.Response.HasStarted)
                {
                    _logger.LogWarning("The response has already started, the http status code middleware will not be executed.");
                    throw;
                }

                context.Response.Clear();
                context.Response.StatusCode = ex.StatusCode;
                context.Response.ContentType = ex.ContentType;

                if (ex.Code == "0" || string.IsNullOrWhiteSpace(ex.Code))
                    ex.Code = ex.StatusCode.ToString();
                var result = new ErrorResult
                {
                    Code = Convert.ToInt32(ex.Code),
                    Status = ex.StatusCode,
                    Message = ex.Message
                };

                JsonSerializerSettings settings = new JsonSerializerSettings
                {
                    ContractResolver = new CamelCasePropertyNamesContractResolver(),
                    NullValueHandling = NullValueHandling.Ignore
                };
                var content = JsonConvert.SerializeObject(result, settings);


                _logger.LogError(content);
                _logger.LogError(ex.StackTrace);
                if (ex.InnerException != null)
                    _logger.LogError(ex.InnerException.ToString());

                await context.Response.WriteAsync(content);

                return;
            }
            catch (Exception ex)
            {
                if (context.Response.HasStarted)
                {
                    _logger.LogWarning("The response has already started, the http status code middleware will not be executed.");
                    throw;
                }

                JsonSerializerSettings settings = new JsonSerializerSettings
                {
                    ContractResolver = new CamelCasePropertyNamesContractResolver(),
                    NullValueHandling = NullValueHandling.Ignore
                };

                string content;

                context.Response.Clear();
                context.Response.ContentType = "application/json";
                if (ex.Message.Contains("isFluentError"))
                {
                    context.Response.StatusCode = StatusCodes.Status400BadRequest;
                    dynamic modifiedFluentErrorResponse = new System.Dynamic.ExpandoObject();
                    var fluentError = JsonConvert.DeserializeObject<FluentErrorResponse>(ex.Message);
                    modifiedFluentErrorResponse.Code = fluentError.Code;
                    modifiedFluentErrorResponse.Status = fluentError.Status;
                    modifiedFluentErrorResponse.Message = fluentError.Message;
                    JArray jArray = new JArray();
                    if (fluentError.Errors != null && fluentError.Errors.Count > 0)
                    {
                        foreach (var item in fluentError.Errors)
                        {
                            JObject jObject = new JObject();
                            jObject.Add(item.Key, item.Value);
                            jArray.Add(jObject);
                        }
                    }
                    modifiedFluentErrorResponse.Errors = jArray;
                    content = JsonConvert.SerializeObject(modifiedFluentErrorResponse, settings);
                }
                else if (ex.GetType().ToString() == typeof(JsonReaderException).FullName && ex.Message.Contains("Could not convert string to DateTime"))
                {
                    context.Response.StatusCode = StatusCodes.Status400BadRequest;
                    var error = new ErrorResult
                    {
                        Code = StatusCodes.Status400BadRequest,
                        Status = StatusCodes.Status400BadRequest,
                        Message = ex.Message
                    };
                    content = JsonConvert.SerializeObject(error, settings);
                }
                else
                {
                    context.Response.StatusCode = StatusCodes.Status500InternalServerError;
                    var error = new ErrorResult
                    {
                        Code = StatusCodes.Status500InternalServerError,
                        Status = StatusCodes.Status500InternalServerError,
                        Message = ex.Message
                    };
                    content = JsonConvert.SerializeObject(error, settings);

                    try
                    {
                        ErrorResult errorResult = JsonConvert.DeserializeObject<ErrorResult>(ex.Message);

                        if (errorResult != null)
                        {
                            //return errorResult;
                            content = JsonConvert.SerializeObject(errorResult, settings);
                            context.Response.StatusCode = errorResult.Status;
                        }
                    }
                    catch (Exception)
                    {
                        //Nothing TODO
                    }
                }

                _logger.LogError(content);
                _logger.LogError(ex.StackTrace);
                if (ex.InnerException != null)
                    _logger.LogError(ex.InnerException.ToString());


                await context.Response.WriteAsync(content);
                return;
            }
            //finally
            //{
            //    Log.CloseAndFlush();
            //}
        }

    }

    /// <summary>
    /// 
    /// </summary>
    // Extension method used to add the middleware to the HTTP request pipeline.
    public static class HttpStatusCodeExceptionMiddlewareExtensions
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="builder"></param>
        /// <returns></returns>
        public static IApplicationBuilder UseHttpStatusCodeExceptionMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<HttpStatusCodeExceptionMiddleware>();
        }
    }
}
