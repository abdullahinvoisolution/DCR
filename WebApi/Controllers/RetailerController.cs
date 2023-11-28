using DCR.Helper.ViewModel;
using DCR.ViewModel.IRepos;
using Microsoft.AspNetCore.Mvc;

namespace DCRWebApi.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class RetailerController : ControllerBase
    {

        private readonly IRetailorRepos _retailorRepos;

        public RetailerController(IRetailorRepos retailorRepos)
        {
            _retailorRepos = retailorRepos;
        }


        [HttpPost]
        public async Task<ActionResult> GetRetailers()
        {

            try
            {
                return Ok(await _retailorRepos.GetRetailers());
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
               "Error Retrieving Data!");

            }

        }


        [HttpPost]
        public async Task<ActionResult<RetailerViewModel>> GetRetailer([FromBody] int RetailerId)
        {
            try
            {
                var Result = await _retailorRepos.GetRetailer(RetailerId);
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
        public async Task<ActionResult> CreateRetailer([FromBody] RetailerViewModel Model)
        {
            try
            {

                if (Model == null)
                {
                    throw new Exception("Null Data!");
                }
                var CreatedRetailer = await _retailorRepos.AddRetailer(Model);
                return StatusCode(StatusCodes.Status201Created, CreatedRetailer);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
               "Error in Creating!");

            }
        }


        [HttpPost]
        public async Task<ActionResult> UpdateRetailer([FromBody] RetailerViewModel Model)
        {
            try
            {
                if (Model == null)
                {
                    throw new Exception("Null Data!");
                }

                var UpdatedRetailer = await _retailorRepos.UpdateRetailer(Model);

                if (UpdatedRetailer != null)
                {
                    return Ok(UpdatedRetailer);
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
        public async Task<ActionResult> DeleteRetailer([FromBody] int RetailerId)
        {
            try
            {
                if (RetailerId == null)
                {
                    throw new Exception("Null Data!");
                }
                var CreatedRetailer = await _retailorRepos.DeleteRetailer(RetailerId);
                return StatusCode(StatusCodes.Status201Created, CreatedRetailer);

            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
               "Error in Creating!");
            }
        }
    }
}
