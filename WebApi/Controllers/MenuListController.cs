using DCR.ViewModel.IRepos;
using DCR.ViewModel.ViewModel;
using Microsoft.AspNetCore.Mvc;

namespace DCRWebApi.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class MenuListController : ControllerBase
    {
        private readonly IMenuListRepos _menuListRepos;

        public MenuListController(IMenuListRepos menuListRepos)
        {
            _menuListRepos = menuListRepos;
        }

        [HttpPost]
        public async Task<ActionResult> GetMenuLists()
        {
            try
            {
                return Ok(await _menuListRepos.GetMenuLists());
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
               "Error Retrieving Data!");

            }

        }

        [HttpPost]
        public async Task<ActionResult<MenuListViewModel>> GetMenuList([FromBody] int MenuId)
        {
            try
            {
                var Result = await _menuListRepos.GetMenuList(MenuId);
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
        public async Task<ActionResult> CreateMenuList([FromBody] MenuListViewModel Model)
        {
            try
            {

                if (Model == null)
                {
                    throw new Exception("Null Data!");
                }
                var CreatedMenuList = await _menuListRepos.AddMenuList(Model);
                return StatusCode(StatusCodes.Status201Created, CreatedMenuList);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
               "Error in Creating!");

            }
        }

        [HttpPost]
        public async Task<ActionResult> UpdateMenuList([FromBody] MenuListViewModel Model)
        {
            try
            {
                if (Model == null)
                {
                    throw new Exception("Null Data!");
                }

                var UpdateMenuList = await _menuListRepos.UpdateMenuList(Model);

                if (UpdateMenuList != null)
                {
                    return Ok(UpdateMenuList);
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
        public async Task<ActionResult> DeleteMenuList([FromBody] int MenuListId)
        {
            try
            {
                if (MenuListId == null)
                {
                    throw new Exception("Null Data!");
                }
                var DeletedMenuList = await _menuListRepos.DeleteMenuList(MenuListId);
                return StatusCode(StatusCodes.Status200OK, DeletedMenuList);

            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
               "Error in Creating!");
            }
        }

    }
}
