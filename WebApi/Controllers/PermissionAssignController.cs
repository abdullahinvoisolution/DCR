//using DAL.EntityModels;
//using DCR.ViewModel.IRepos;
//using DCR.ViewModel.ViewModel;
//using Microsoft.AspNetCore.Http;
//using Microsoft.AspNetCore.Mvc;

//namespace DCRWebApi.Controllers
//{
//    [Route("api/[controller]/[action]")]
//    [ApiController]
//    public class PermissionAssignController : ControllerBase
//    {
//        private readonly IMenuPermissionsAsignRepos _menuPermissionsAsignRepos;

//        public PermissionAssignController(IMenuPermissionsAsignRepos menuPermissionsAsignRepos)
//        {
//            _menuPermissionsAsignRepos = menuPermissionsAsignRepos;
//        }


//        [HttpPost]
//        public async Task<ActionResult> GetMenuPermissions()
//        {

//            try
//            {
//                return Ok(await _menuPermissionsAsignRepos.GetMenuPermissions());
//            }
//            catch (Exception)
//            {
//                return StatusCode(StatusCodes.Status500InternalServerError,
//               "Error Retrieving Data!");

//            }

//        }


//        [HttpPost]
//        public async Task<ActionResult<MenuPermissionAssign>> GetMenuPermission(int MenuAssignId)
//        {
//            try
//            {
//                var result = await _menuPermissionsAsignRepos.GetMenuPermission(MenuAssignId);
//                if (result == null)
//                {
//                    return NotFound();
//                }
//                return result;
//            }
//            catch (Exception)
//            {
//                return StatusCode(StatusCodes.Status500InternalServerError,
//               "Error Retrieving Data!");

//            }

//        }

//        [HttpPost]
//        public async Task<ActionResult<MenuPermissionAssign>> CreateMenuPermission([FromBody] MenuPermissionAssignViewModel model)
//        {
//            try
//            {

//                if (model == null)
//                {
//                    return BadRequest();
//                }
//                var CreatedPermission = await _menuPermissionsAsignRepos.AddMenuPermission(model);
//                return CreatedAtAction(nameof(GetMenuPermission), new { id = CreatedPermission.Id }, CreatedPermission);
//            }
//            catch (Exception)
//            {
//                return StatusCode(StatusCodes.Status500InternalServerError,
//               "Error in Creating!");

//            }
//        }


//        [HttpPost]
//        public async Task<ActionResult<MenuPermissionAssign>> UpdateMenuPermission(int MenuAssignId, [FromBody] MenuPermissionAssignViewModel model)
//        {
//            try
//            {
//                if (model == null)
//                {
//                    return BadRequest("Invalid data. Please provide valid User Role data.");
//                }

//                // Assuming you have a method like UpdateCustomer in your _customerRepos
//                var UpdatePermissionAssign = await _menuPermissionsAsignRepos.UpdateMenuPermission(MenuAssignId, model);

//                if (UpdatePermissionAssign != null)
//                {
//                    return Ok(UpdatePermissionAssign); // Return 200 OK with the updated customer
//                }
//                else
//                {
//                    return NotFound("User Role not found"); // Return 404 Not Found if the customer doesn't exist
//                }
//            }
//            catch (Exception ex)
//            {
//                return StatusCode(StatusCodes.Status500InternalServerError, "Error in Updating: " + ex.Message);
//            }
//        }


//        [HttpPost]
//        public async Task<ActionResult<MenuPermissionAssign>> DeleteMenuPermission([FromBody] int MenuAssignId)
//        {
//            try
//            {
//                if (MenuAssignId == null)
//                {
//                    return BadRequest();
//                }
//                var DeletedPermission = await _menuPermissionsAsignRepos.DeleteMenuPermission(MenuAssignId);
//                return CreatedAtAction(nameof(DeletedPermission), new { id = DeletedPermission.Id }, DeletedPermission);
//            }
//            catch (Exception)
//            {
//                return StatusCode(StatusCodes.Status500InternalServerError,
//               "Error in Creating!");
//            }
//        }
//    }
//}
