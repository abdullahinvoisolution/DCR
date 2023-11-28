using DCR.ViewModel.IRepos;
using DCR.ViewModel.ViewModel;
using Microsoft.AspNetCore.Mvc;

namespace DCRWebApi.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class SaleOrderLineController : ControllerBase
    {
        private readonly ISaleOrderLineRepos _saleOrderLineRepos;

        public SaleOrderLineController(ISaleOrderLineRepos saleOrderLineRepos)
        {
            _saleOrderLineRepos = saleOrderLineRepos;
        }

        [HttpPost]
        public async Task<ActionResult> GetSaleOrderLines()
        {

            try
            {
                return Ok(await _saleOrderLineRepos.GetSaleOrderLines());
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
               "Error Retrieving Data!");

            }

        }

        [HttpPost]
        public async Task<ActionResult<SaleOrderLineViewModel>> GetSaleOrderLine([FromBody] int SaleOrderLineId)
        {
            try
            {
                var Result = await _saleOrderLineRepos.GetSaleOrderLine(SaleOrderLineId);
                if (Result == null)
                {
                    throw new Exception("Null Data!");
                }
                return Result;
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
               "Error Retrieving Data!");

            }

        }

        [HttpPost]
        public async Task<ActionResult> CreateSaleOrderLine([FromBody] SaleOrderLineViewModel Model)
        {
            try
            {

                if (Model == null)
                {
                    throw new Exception("Null Data!");
                }
                var CreatedSalesOrderLine = await _saleOrderLineRepos.AddSaleOrderLine(Model);
                return StatusCode(StatusCodes.Status201Created, CreatedSalesOrderLine);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
               "Error in Creating!");

            }
        }

        [HttpPost]
        public async Task<ActionResult> UpdateSaleOrderLine([FromBody] SaleOrderLineViewModel Model)
        {
            try
            {
                if (Model == null)
                {
                    throw new Exception("Null Data!"); 
                }

                var UpdateSaleOrderLine = await _saleOrderLineRepos.UpdateSaleOrderLine(Model);

                if (UpdateSaleOrderLine != null)
                {
                    throw new Exception("Null Data!");
                }
                else
                {
                    throw new Exception("Null Data!");
                }
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error in Updating: " + ex.Message);
            }
        }

        [HttpPost]
        public async Task<ActionResult> DeleteSaleOrderLine([FromBody] int SaleOrderLineId)
        {
            try
            {
                if (SaleOrderLineId == null)
                {
                    throw new Exception("Null Data!"); ;
                }
                var CreatedSaleOrderLine = await _saleOrderLineRepos.DeleteSaleOrderline(SaleOrderLineId);
                return StatusCode(StatusCodes.Status201Created, CreatedSaleOrderLine);

            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
               "Error in Creating!");
            }
        }
    }
}
