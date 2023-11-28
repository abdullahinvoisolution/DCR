using DCR.Helper.ViewModel;
using DCR.ViewModel.IRepos;
using Microsoft.AspNetCore.Mvc;

namespace DCRWebApi.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class DistributorController : ControllerBase
    {
        private readonly IDistributorRepos _distributorRepos;

        public DistributorController(IDistributorRepos distributorRepos)
        {
            _distributorRepos = distributorRepos;
        }

        [HttpPost]
        public async Task<ActionResult> GetDistributors()
        {

            try
            {
                return Ok(await _distributorRepos.GetDistributors());
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
               "Error Retrieving Data!");

            }

        }

        [HttpPost]
        public async Task<ActionResult<DistributorViewModel>> GetDistributor([FromBody] int DistributorId)
        {
            try
            {
                var result = await _distributorRepos.GetDistributor(DistributorId);
                if (result == null)
                {
                    throw new Exception("Null Data!");
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
        public async Task<ActionResult> CreateDistributor([FromBody] DistributorViewModel Model)
        {
            try
            {

                if (Model == null)
                {
                    throw new Exception("Null Data!");
                }
                var CreatedUser = await _distributorRepos.AddDistributor(Model);
                return StatusCode(StatusCodes.Status201Created, CreatedUser);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
               "Error in Creating!");

            }
        }

        [HttpPost]
        public async Task<ActionResult> UpdateCustomer([FromBody] DistributorViewModel Model)
        {
            try
            {
                if (Model == null)
                {
                throw new Exception("Null Data!");
                }

                var UpdatedDistributor = await _distributorRepos.UpdateDistributor(Model);

                if (UpdatedDistributor != null)
                {
                    return Ok(UpdatedDistributor);
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
        public async Task<ActionResult> DeleteCustomer([FromBody] int DistributorId)
        {
            try
            {
                if (DistributorId == null)
                {
                    return BadRequest();
                }
                var CreatedDistributor = await _distributorRepos.DeleteDistributor(DistributorId);
                return StatusCode(StatusCodes.Status201Created, CreatedDistributor);

            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
               "Error in Creating!");
            }
        }
    }
}
