using DCR.Helper.ViewModel;
using DCR.ViewModel.IRepos;
using Microsoft.AspNetCore.Mvc;

namespace DCRWebApi.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class WarehouseController : ControllerBase
    {
        private readonly IWarehouseRepos _warehouseRepos;

        public WarehouseController(IWarehouseRepos warehouseRepos)
        {
            _warehouseRepos = warehouseRepos;
        }
        [HttpPost]
        public async Task<ActionResult> GetWarehouses()
        {

            try
            {
                return Ok(await _warehouseRepos.GetWarehouses());
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
               "Error Retrieving Data!");

            }

        }

        [HttpPost]
        public async Task<ActionResult<object>> GetWarehouse(int WarehouseId)
        {
            try
            {
                var result = await _warehouseRepos.GetWarehouse(WarehouseId);
                if (result == null)
                {
                    return NotFound();
                }
                return result;
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
               "Error Retrieving Data!");

            }

        }

        [HttpPost]
        public async Task<ActionResult<object>> CreateWarehouse([FromBody] WarehouseViewModel model)
        {
            try
            {

                if (model == null)
                {
                    return BadRequest();
                }
                var CreatedUser = await _warehouseRepos.AddWarehouse(model);
                return CreatedAtAction(nameof(GetWarehouse), new { id = CreatedUser.Name }, CreatedUser);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
               "Error in Creating!");

            }
        }

        [HttpPost]
        public async Task<ActionResult<object>> UpdateWarehouse(int WarehouseId, [FromBody] WarehouseViewModel model)
        {
            try
            {
                if (model == null)
                {
                    return BadRequest("Invalid data. Please provide valid customer data.");
                }

                // Assuming you have a method like UpdateCustomer in your _customerRepos
                var updatedWarehouse = await _warehouseRepos.UpdateWarehouse(WarehouseId, model);

                if (updatedWarehouse != null)
                {
                    return Ok(updatedWarehouse); // Return 200 OK with the updated customer
                }
                else
                {
                    return NotFound("Customer not found"); // Return 404 Not Found if the customer doesn't exist
                }
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error in Updating: " + ex.Message);
            }
        }


        [HttpPost]
        public async Task<ActionResult<object>> DeleteCustomer([FromBody] int WarehouseId)
        {
            try
            {
                if (WarehouseId == null)
                {
                    return BadRequest();
                }
                var CreatedWarehouse = await _warehouseRepos.DeleteWarehouse(WarehouseId);
                return StatusCode(StatusCodes.Status201Created, CreatedWarehouse);

            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
               "Error in Creating!");
            }
        }

    }
}
