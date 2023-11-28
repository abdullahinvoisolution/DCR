using DCR.Helper.ViewModel;
using DCR.ViewModel.IRepos;
using Microsoft.AspNetCore.Mvc;

namespace DCRWebApi.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class PurchaseOrderController : ControllerBase
    {
        private readonly IPurchaseOrderRepos _purchaseOrderRepos;

        public PurchaseOrderController(IPurchaseOrderRepos purchaseOrderRepos)
        {
            _purchaseOrderRepos = purchaseOrderRepos;
        }



        [HttpPost]
        public async Task<ActionResult> GetPurchaseOrders()
        {

            try
            {
                return Ok(await _purchaseOrderRepos.GetPurchaseOrders());
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
               "Error Retrieving Data!");

            }

        }



        [HttpPost]
        public async Task<ActionResult<PurchaseOrderViewModel>> GetPurchaeOrder([FromBody] int PurchaseId)
        {
            try
            {
                var Result = await _purchaseOrderRepos.GetPurchaseOrder(PurchaseId);
                if (Result == null)
                {
                    return NotFound();
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
        public async Task<ActionResult> CreatePurchaseOrder([FromBody] PurchaseOrderViewModel Model)
        {
            try
            {

                if (Model == null)
                {
                    throw new Exception("Null Data!");
                }
                var CreatedPurchaseOrder = await _purchaseOrderRepos.AddPurchaseOrder(Model);
                return StatusCode(StatusCodes.Status201Created, CreatedPurchaseOrder);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
               "Error in Creating!");
            }
        }


        [HttpPost]
        public async Task<ActionResult> UpdatePurchaseOrder([FromBody] PurchaseOrderViewModel Model)
        {
            try
            {
                if (Model == null)
                {
                    throw new Exception("Null Data!");
                }
                var UpdatePurchaseOrder = await _purchaseOrderRepos.UpdatePurchaseOrder(Model);

                if (UpdatePurchaseOrder != null)
                {
                    return Ok(UpdatePurchaseOrder); 
                }
                else
                {
                    throw new Exception("No Data Found!");
                }
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error in Updating: " + ex.Message);
            }
        }



        [HttpPost]
        public async Task<ActionResult> DeletePurchaseOrder([FromBody] int PurchaseId)
        {
            try
            {
                if (PurchaseId == null)
                {
                    throw new Exception("Null Data!");
                }
                var CreatedPurchaseOrder = await _purchaseOrderRepos.DeletePurchaseOrder(PurchaseId);
                return StatusCode(StatusCodes.Status201Created, CreatedPurchaseOrder);

            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
               "Error in Creating!");
            }
        }

    }
}
