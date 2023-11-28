using DCR.ViewModel.IRepos;
using DCR.ViewModel.ViewModel;
using Microsoft.AspNetCore.Mvc;

namespace DCRWebApi.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ReceiveController : ControllerBase
    {

        private readonly IReceiveRepos _receiveRepos;

        public ReceiveController(IReceiveRepos receiveRepos)
        {
            _receiveRepos = receiveRepos;
        }

        [HttpPost]
        public async Task<ActionResult> GetReceives()
        {

            try
            {
                return Ok(await _receiveRepos.GetReceives());
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
               "Error Retrieving Data!");

            }

        }

        [HttpPost]
        public async Task<ActionResult<ReceiveViewModel>> GetReceive([FromBody] int ReceiveId)
        {
            try
            {
                var Result = await _receiveRepos.GetReceive(ReceiveId);
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
        public async Task<ActionResult> CreateReceive([FromBody] ReceiveViewModel Model)
        {
            try
            {

                if (Model == null)
                {
                    return BadRequest();
                }
                var CreatedRecieve = await _receiveRepos.AddReceive(Model);
                return StatusCode(StatusCodes.Status201Created, CreatedRecieve);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
               "Error in Creating!");
            }
        }


        [HttpPost]
        public async Task<ActionResult> UpdateReceive([FromBody] ReceiveViewModel Model)
        {
            try
            {
                if (Model == null)
                {
                    throw new Exception("Null Data!");
                }

                var UpdatedReceive = await _receiveRepos.UpdateReceive(Model);

                if (UpdatedReceive != null)
                {
                    return Ok(UpdatedReceive); 
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
        public async Task<ActionResult> DeleteReceive([FromBody] int ReceiveId)
        {
            try
            {
                if (ReceiveId == null)
                {
                    return BadRequest();
                }
                var CreatedRecieve = await _receiveRepos.DeleteReceive(ReceiveId);
                return StatusCode(StatusCodes.Status201Created, CreatedRecieve);

            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
               "Error in Creating!");
            }
        }

    }
}
