using DCR.ViewModel.IRepos;
using DCR.ViewModel.ViewModel;
using Microsoft.AspNetCore.Mvc;

namespace DCRWebApi.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class PurchaseOrderLineController : ControllerBase
    {
        private readonly IPurchaseOrderline _purchaseOrderline;

        public PurchaseOrderLineController(IPurchaseOrderline purchaseOrderline)
        {
            _purchaseOrderline = purchaseOrderline;
        }

        [HttpPost]
        public async Task<ActionResult> GetPurchaseOrderLines()
        {

            try
            {
                return Ok(await _purchaseOrderline.GetPurchaseOrderLines());
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
               "Error Retrieving Data!");

            }

        }

        [HttpPost]
        public async Task<ActionResult<PurchaseOrderLineViewModel>> GetPurchaseOrderLine([FromBody] int PurchaseOrderLineId)
        {
            try
            {
                var Result = await _purchaseOrderline.GetPurchaseOrderLine(PurchaseOrderLineId);
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
        public async Task<ActionResult> CreatePurchaseOrderLine([FromBody] PurchaseOrderLineViewModel Model)
        {
            try
            {

                if (Model == null)
                {
                    return BadRequest();
                }
                var CreatedPurchaseOrderLine = await _purchaseOrderline.AddPurchaseOrderLine(Model);
                return StatusCode(StatusCodes.Status201Created, CreatedPurchaseOrderLine);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
               "Error in Creating!");

            }
        }


        [HttpPost]
        public async Task<ActionResult> UpdatePurchaseOrderLine([FromBody] PurchaseOrderLineViewModel Model)
        {
            try
            {
                if (Model == null)
                {
                    throw new Exception("Null Data!");
                }
                var UpdatedPurchaseOrderLine = await _purchaseOrderline.UpdatePurchaseOrderLine(Model);

                if (UpdatedPurchaseOrderLine != null)
                {
                    return Ok(UpdatedPurchaseOrderLine); 
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
        public async Task<ActionResult> DeletePurchaseOrderLine([FromBody] int PurchaseOrderLineId)
        {
            try
            {
                if (PurchaseOrderLineId == null)
                {
                    throw new Exception("Null Data!");
                }
                var CreatedPurchaseOrderLine = await _purchaseOrderline.DeletePurchaseOrderLine(PurchaseOrderLineId);
                return StatusCode(StatusCodes.Status201Created, CreatedPurchaseOrderLine);

            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
               "Error in Creating!");
            }
        }
    }
}
