using Microsoft.AspNetCore.Mvc;
using Stockexchange.Model.ResponseModel;
using Stockexchange.Service.StockExchangeRepository.Interface;
using Stockexchangeapi.Helper;
using System.ComponentModel.DataAnnotations;

namespace Stockexchangeapi.Controllers
{
    /// <summary>
    /// StockDetailController
    /// </summary>
    [Route("api/stock")]
    [ApiController]
    public class StockDetailController : ControllerBase
    {
        private IStockDetailRepository _stockDetailRepository;
        /// <summary>
        /// Intializing StockDetail Controller 
        /// </summary>
        /// <param name="stockDetailRepository">StockDetailRepository</param>
        public StockDetailController(IStockDetailRepository stockDetailRepository)
        {
            _stockDetailRepository = stockDetailRepository;

        }

        #region GetByID
        /// <summary>
        /// Get Stock Detail
        /// </summary>           
        /// <response code="200">OK: The request was successful and the response body contains the representation requested.</response>
        /// <response code="400">BAD REQUEST: The data given in the POST or PUT failed validation. Inspect the response body for details.</response>
        /// <response code="401">UNAUTHORIZED: The supplied credentials, if any, are not sufficient to access the resource.</response>
        /// <response code="404">NOT FOUND</response>
        /// <response code="500">SERVER ERROR: We couldn't return the representation due to an internal server error.</response>
        [HttpGet]
        [ProducesResponseType(typeof(GetStockResponseModel), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(UnauthorizedResponse), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(NotFoundResponse), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(BadRequestResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(InternalServerErrorReponse), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetById([FromQuery][Required] string ticker_symbol)
        {
            var result = await _stockDetailRepository.Get(ticker_symbol);

            return Ok(result);

        }
        #endregion
    }
}
