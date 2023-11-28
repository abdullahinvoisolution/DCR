using DCR.Helper.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Repository.IRepos;

namespace DCRWebApi.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class RoleController : ControllerBase
    {
        private readonly IRoleRepos _roleRepos;
        public RoleController(IRoleRepos roleRepos)
        {
            _roleRepos = roleRepos;
        }

        [HttpPost]
        public async Task<ActionResult> GetRoles()
        {

            try
            {
                return Ok(await _roleRepos.GetRoles());
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
               "Error Retrieving Data!");

            }

        }

        [HttpPost]
        public async Task<ActionResult<RoleViewModel>> GetRole([FromBody] int RoleId)
        {
            try
            {
                var Result = await _roleRepos.GetRole(RoleId);
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
        public async Task<ActionResult> CreateCustomer([FromBody] RoleViewModel Model)
        {
            try
            {
                if (Model == null)
                {
                    throw new Exception("Null Data!");
                }
                var CreatedRoles = await _roleRepos.AddRole(Model);
                return StatusCode(StatusCodes.Status201Created, CreatedRoles);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
               "Error in Creating!");

            }
        }

        [HttpPost]
        public async Task<ActionResult> UpdateRole([FromBody] RoleViewModel Model)
        {
            try
            {
                if (Model == null)
                {
                    throw new Exception("Null Data!");
                }

                var UpdatedCustomer = await _roleRepos.UpdateRole(Model);

                if (UpdatedCustomer != null)
                {
                    return Ok(UpdatedCustomer); 
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
        public async Task<ActionResult> DeleteRole([FromBody] int RoleId)
        {
            try
            {
                if (RoleId == null)
                {
                    throw new Exception("Null Data!"); ;
                }
                var CreatedRole = await _roleRepos.DeleteRole(RoleId);
                return StatusCode(StatusCodes.Status201Created, CreatedRole);


            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
               "Error in Creating!");
            }
        }

    }
}
