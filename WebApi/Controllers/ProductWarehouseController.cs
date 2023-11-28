using DCR.ViewModel.IRepos;
using DCR.ViewModel.ViewModel;
using Microsoft.AspNetCore.Mvc;

namespace DCRWebApi.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ProductWarehouseController : ControllerBase
    {
        private readonly IProductWarehouseRepos _productWarehouseRepos;

        public ProductWarehouseController(IProductWarehouseRepos productWarehouseRepos)
        {
            _productWarehouseRepos = productWarehouseRepos;
        }

        [HttpPost]
        public async Task<ActionResult> GetProductWarehouses()
        {

            try
            {
                return Ok(await _productWarehouseRepos.GetProductWarehouses());
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
               "Error Retrieving Data!");

            }

        }


        [HttpPost]
        public async Task<ActionResult<ProductWarehouseViewModel>> GetProductWarehouse([FromBody] int ProductWarehouseId)
        {
            try
            {
                var Result = await _productWarehouseRepos.GetProductWarehouse(ProductWarehouseId);
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
        public async Task<ActionResult> CreateProductWarehouse([FromBody] ProductWarehouseViewModel Model)
        {
            try
            {

                if (Model == null)
                {
                    throw new Exception("Null Data!");
                }
                var CreatedProductWarehouse = await _productWarehouseRepos.AddProductWarehouse(Model);
                return StatusCode(StatusCodes.Status201Created, CreatedProductWarehouse);

            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
               "Error in Creating!");

            }
        }


        [HttpPost]
        public async Task<ActionResult> UpdateProductWarehouse([FromBody] ProductWarehouseViewModel Model)
        {
            try
            {
                if (Model == null)
                {
                    return BadRequest("Invalid data. Please provide valid Product Warehouse data.");
                }

                var UpdateProductWarehouse = await _productWarehouseRepos.UpdateProductWarehouse(Model);

                if (UpdateProductWarehouse != null)
                {
                    return Ok(UpdateProductWarehouse); 
                }
                else
                {
                    return NotFound("Product Warehouse not found");
                }
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error in Updating: " + ex.Message);
            }
        }



        [HttpPost]
        public async Task<ActionResult> DeleteProductWarehouse([FromBody] int ProductWarehouseId)
        {
            try
            {
                if (ProductWarehouseId == null)
                {
                    throw new Exception("Null Data!");
                }
                var CreatedProductWarehouse = await _productWarehouseRepos.DeleteProductWarehouse(ProductWarehouseId);
                return StatusCode(StatusCodes.Status201Created, CreatedProductWarehouse);

            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
               "Error in Creating!");
            }
        }

    }
}
