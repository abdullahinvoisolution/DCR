using DCR.ViewModel.IRepos;
using DCR.ViewModel.ViewModel;
using Microsoft.AspNetCore.Mvc;

namespace DCRWebApi.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class InventoryController : ControllerBase
    {
        private readonly IInventoryRepos _inventoryRepos;

        public InventoryController(IInventoryRepos inventoryRepos)
        {
            _inventoryRepos = inventoryRepos;
        }

        [HttpPost]
        public async Task<ActionResult> GetInventories()
        {

            try
            {
                return Ok(await _inventoryRepos.GetInventories());
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
               "Error Retrieving Data!");

            }

        }

        [HttpPost]
        public async Task<ActionResult<InventoryViewModel>> GetInventory([FromBody] int InventoryId)
        {
            try
            {
                var Result = await _inventoryRepos.GetInventory(InventoryId);
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
        public async Task<ActionResult> CreateInventory([FromBody] InventoryViewModel Model)
        {
            try
            {

                if (Model == null)
                {
                    throw new Exception("Null Data!");
                }
                var CreatedInventory = await _inventoryRepos.AddInventory(Model);
                return StatusCode(StatusCodes.Status201Created, CreatedInventory);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
               "Error in Creating!");

            }
        }

        [HttpPost]
        public async Task<ActionResult> UpdateInventory([FromBody] InventoryViewModel Model)
        {
            try
            {
                if (Model == null)
                {
                    return BadRequest("Invalid data. Please provide valid customer data.");
                }

                var UpdatedInventory = await _inventoryRepos.UpdateInventory(Model);

                if (UpdatedInventory != null)
                {
                    return Ok(UpdatedInventory); 
                }
                else
                {
                    throw new Exception("Data Not Found!");
                }
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error in Updating: " + ex.Message);
            }
        }


        [HttpPost]
        public async Task<ActionResult> DeleteInventory([FromBody] int InventoryId)
        {
            try
            {
                if (InventoryId == null)
                {
                    throw new Exception("Null Data!");
                }
                var CreatedInventory = await _inventoryRepos.DeleteInventory(InventoryId);
                return StatusCode(StatusCodes.Status201Created, CreatedInventory);

            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
               "Error in Creating!");
            }
        }
    }
}
