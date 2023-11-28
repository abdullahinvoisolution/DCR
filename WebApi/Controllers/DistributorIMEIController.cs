using DCR.ViewModel.IRepos;
using DCR.ViewModel.ViewModel;
using Microsoft.AspNetCore.Mvc;

namespace DCRWebApi.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class DistributorIMEIController : ControllerBase
    {
        private readonly IDistributorIMEIRepos _distributorIMEIRepos ;

        public DistributorIMEIController(IDistributorIMEIRepos distributorIMEIRepos)
        {
            _distributorIMEIRepos = distributorIMEIRepos ;
        }

        [HttpPost]
        public async Task<ActionResult> GetDistributorIMEIS()
        {

            try
            {
                return Ok(await _distributorIMEIRepos.GetDistributorImeis());
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
               "Error Retrieving Data!");

            }

        }


        [HttpPost]
        public async Task<ActionResult<DistibutorIMEIViewModel>> GetDistributorIMEI([FromBody] int DistributorImeiId)
        {
            try
            {
                var Result = await _distributorIMEIRepos.GetDistributorImei(DistributorImeiId);
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
        public async Task<ActionResult> CreateDistributorIMEI([FromBody] DistibutorIMEIViewModel Model)
        {
            try
            {

                if (Model == null)
                {
                    throw new Exception("Null Data!");
                }
                var CreatedDistributerImei = await _distributorIMEIRepos.AddDistributorImei(Model);
                return StatusCode(StatusCodes.Status201Created, CreatedDistributerImei);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
               "Error in Creating!");

            }
        }



        [HttpPost]
        public async Task<ActionResult> UpdateDistributorIMEI([FromBody] DistibutorIMEIViewModel Model)
        {
            try
            {
                if (Model == null)
                {
                    return BadRequest("Invalid data. Please provide Distributor IMEI data.");
                }

                var UpdateDistributorIMEI = await _distributorIMEIRepos.UpdateDistributorImei(Model);

                if (UpdateDistributorIMEI != null)
                {
                    return Ok(UpdateDistributorIMEI); 
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
        public async Task<ActionResult> DeleteDistributorIMEI([FromBody] int DistributorImeiId)
        {
            try
            {
                if (DistributorImeiId == null)
                {
                    throw new Exception("Null Data!");
                }
                var CreatedDistributorIMEI = await _distributorIMEIRepos.DeleteDistributorImei(DistributorImeiId);
                return StatusCode(StatusCodes.Status201Created, CreatedDistributorIMEI);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
               "Error in Creating!");
            }
        }

    }
}
