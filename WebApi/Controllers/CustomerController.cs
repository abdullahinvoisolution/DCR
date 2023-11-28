using DCR.Helper.ViewModel;
using DCR.ViewModel.IRepos;
using Microsoft.AspNetCore.Mvc;

namespace DCRWebApi.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly ICustomerRepos _customerRepos;

        public CustomerController(ICustomerRepos customerRepos)
        {
            _customerRepos = customerRepos;
        }


        [HttpPost]
        public async Task<ActionResult> GetCustomers()
        {

            try
            {
                return Ok(await _customerRepos.GetCustomers());
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
               "Error Retrieving Data!");

            }

        }



        [HttpPost]
        public async Task<ActionResult<CustomerViewModel>> GetCustomer([FromBody] int CustomerId)
        {
            try
            {
                var Result = await _customerRepos.GetCustomer(CustomerId);
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
        public async Task<ActionResult> CreateCustomer([FromBody] CustomerViewModel Model)
        {
            try
            {
                if (Model == null)
                {
                    throw new Exception("Null Data!");
                }
                var CreatedCustomer = await _customerRepos.AddCustomer(Model);
                return StatusCode(StatusCodes.Status201Created, CreatedCustomer);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
               "Error in Creating!");

            }
        }


        [HttpPost]
        public async Task<ActionResult> UpdateCustomer([FromBody] CustomerViewModel Model)
        {
            try
            {
                if (Model == null)
                {
                    throw new Exception("Null Data!");
                }

                var UpdatedCustomer = await _customerRepos.UpdateCustomer(Model);
                
                if (UpdatedCustomer != null)
                {
                    return Ok(UpdatedCustomer); 
                }
                else
                {
                    throw new Exception("Customer not found!");
                }
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error in Updating: " + ex.Message);
            }
        }

        [HttpPost]
        public async Task<ActionResult> DeleteCustomer([FromBody] int CustomerId)
        {
            try
            {
                if (CustomerId == null)
                {
                    throw new Exception("Null Data!");
                }
                var CreatedCustomer = await _customerRepos.DeleteCustomer(CustomerId);
                return StatusCode(StatusCodes.Status201Created, CreatedCustomer);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
               "Error in Creating!");
            }
        }

    }
}
