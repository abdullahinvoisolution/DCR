using DCR.ViewModel.IRepos;
using DCR.ViewModel.ViewModel;
using Microsoft.AspNetCore.Mvc;

namespace DCRWebApi.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class PurchaseOrderDetailController : ControllerBase
    {
        private readonly IPurchaseOrderDetailRepos _purchaseOrderDetailRepos;

        public PurchaseOrderDetailController(IPurchaseOrderDetailRepos purchaseOrderDetailRepos)
        {
            _purchaseOrderDetailRepos = purchaseOrderDetailRepos;
        }

        [HttpPost]
        public async Task<ActionResult> GetPurchaseOrderDetails()
        {

            try
            {
                return Ok(await _purchaseOrderDetailRepos.GetPurchaseOrderDetails());
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
               "Error Retrieving Data!");

            }

        }

        [HttpPost]
        public async Task<ActionResult<PurchaseOrderDetailViewModel>> GetPurchaseOrderDetail([FromBody] int PurchaseOrderDetailId)
        {
            try
            {
                var Result = await _purchaseOrderDetailRepos.GetPurchaseOrderDetail(PurchaseOrderDetailId);
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
        public async Task<ActionResult> CreatePurchaseOrderDetail([FromBody] PurchaseOrderDetailViewModel Model)
        {
            try
            {

                if (Model == null)
                {
                    throw new Exception("Null Data!");
                }
                var CreatedPurchaseOrderDetails = await _purchaseOrderDetailRepos.AddPurchaseOrderDetail(Model);
                return StatusCode(StatusCodes.Status201Created, CreatedPurchaseOrderDetails);
            }

            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
               "Error in Creating!");

            }
        }


        [HttpPost]
        public async Task<ActionResult> UpdatePurchaseOrderDetail([FromBody] PurchaseOrderDetailViewModel Model)
        {
            try
            {
                if (Model == null)
                {
                    throw new Exception("Null Data!");
                }

                var UpdatedPurchaseOrderDetail = await _purchaseOrderDetailRepos.UpdatePurchaseOrderDetail(Model);

                if (UpdatedPurchaseOrderDetail != null)
                {
                    return Ok(UpdatedPurchaseOrderDetail);
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
        public async Task<ActionResult> DeletePayment([FromBody] int PurchaseOrderDetailId)
        {
            try
            {
                if (PurchaseOrderDetailId == null)
                {
                    throw new Exception("Null Data!");
                }
                var CreatedPurchaseOrderDetail = await _purchaseOrderDetailRepos.DeletePurchaseOrderDetail(PurchaseOrderDetailId);
                return StatusCode(StatusCodes.Status201Created, CreatedPurchaseOrderDetail);

            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
               "Error in Creating!");
            }
        }
    }
}
