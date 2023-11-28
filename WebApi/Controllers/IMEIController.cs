using DCR.Helper.ViewModel;
using DCR.ViewModel.IRepos;
using Microsoft.AspNetCore.Mvc;


namespace DCRWebApi.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class IMEIController : ControllerBase
    {
        private readonly IIMEIRepos _iMEIRepos;

        public IMEIController(IIMEIRepos iMEIRepos)
        {
            _iMEIRepos = iMEIRepos;
        }


        [HttpPost]
        public async Task<ActionResult> GetIMEIs()
        {

            try
            {
                return Ok(await _iMEIRepos.GetIMEIs());
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
               "Error Retrieving Data!");

            }

        }

        [HttpPost]
        public async Task<ActionResult<IMEIViewModel>> GetIMEI([FromBody] int ImeiId)
        {
            try
            {
                var Result = await _iMEIRepos.GetIMEI(ImeiId);
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
        public async Task<ActionResult> CreateIMEI([FromBody] IMEIViewModel Model)
        {
            try
            {

                if (Model == null)
                {
                    throw new Exception("Null Data!");
                }
                var CreatedImei = await _iMEIRepos.AddIMEI(Model);
                return StatusCode(StatusCodes.Status201Created, CreatedImei);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
               "Error in Creating!");

            }
        }


        [HttpPost]
        public async Task<ActionResult> UpdateImei([FromBody] IMEIViewModel Model)
        {
            try
            {
                if (Model == null)
                {
                    throw new Exception("Null Data!");
                }

                var UpdateImei = await _iMEIRepos.UpdateIMEI(Model);

                if (UpdateImei != null)
                {
                    return Ok(UpdateImei);
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
        public async Task<ActionResult> DeleteIMEI([FromBody] int ImeiId)
        {
            try
            {
                if (ImeiId == null)
                {
                    throw new Exception("Null Data!");
                }
                var CreatedIMEI = await _iMEIRepos.DeleteIMEI(ImeiId);
                return StatusCode(StatusCodes.Status201Created, CreatedIMEI);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
               "Error in Creating!");
            }
        }


    }
}
