using DAL.EntityModels;
using DCR.ViewModel.IRepos;
using DCR.ViewModel.ViewModel;
using Microsoft.EntityFrameworkCore;


namespace Repository.Repos
{
    public class MenuListRepos : IMenuListRepos
    {
        private readonly EMS_ITCContext _context;

        public MenuListRepos(EMS_ITCContext context)
        {
            _context = context;
        }

        public async Task<bool> AddMenuList(MenuListViewModel Model)
        {
            try
            {
                var NewMenuList = new MenuList
                {
                    MenuListTitle = Model.MenuListTitle,
                    MenuListDescription = Model.MenuListDescription,
                    MenuListParentId = Model.MenuListParentId,
                    MenuListHasChildren = Model.MenuListHasChildren,
                    MenuListSortOrder = Model.MenuListSortOrder,
                    MenuListNavigationUrl = Model.MenuListNavigationUrl,
                    MenuListIconClass = Model.MenuListIconClass,
                    IsActive = true
                };

                _context.MenuLists.Add(NewMenuList);
                await _context.SaveChangesAsync();


                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<bool> DeleteMenuList(int MenuId)
        {
            try
            {
                var Result = await _context.MenuLists.Where(a => a.MenuListId == MenuId).FirstOrDefaultAsync();
                if (Result != null)
                {
                    Result.IsActive = false;
                    await _context.SaveChangesAsync();
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<MenuListViewModel> GetMenuList(int MenuId)
        {
            try
            {
                var Result = await _context.MenuLists.FirstOrDefaultAsync(a => a.MenuListId == MenuId && a.IsActive == true);

                if (Result != null)
                {
                    var Model = new MenuListViewModel
                    {
                        MenuListTitle = Result.MenuListTitle,
                        MenuListDescription = Result.MenuListDescription,
                        MenuListParentId = Result.MenuListParentId,
                        MenuListHasChildren = Result.MenuListHasChildren,
                        MenuListSortOrder = Result.MenuListSortOrder,
                        MenuListNavigationUrl = Result.MenuListNavigationUrl,
                        MenuListIconClass = Result.MenuListIconClass
                    };

                    return Model;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception)
            {

                return null;
            }
        }

        public async Task<List<MenuListViewModel>> GetMenuLists()
        {
            try
            {
                MenuListViewModel Model = new MenuListViewModel();
                IEnumerable<MenuListViewModel> MenuLists = _context.MenuLists
                      .Where(x => x.IsActive == true)
                      .Select(x => new MenuListViewModel
                      {
                          MenuListId = x.MenuListId,
                          MenuListTitle = x.MenuListTitle,
                          MenuListDescription = x.MenuListDescription,
                          MenuListParentId = x.MenuListParentId,
                          MenuListHasChildren = x.MenuListHasChildren,
                          MenuListSortOrder = x.MenuListSortOrder,
                          MenuListNavigationUrl = x.MenuListNavigationUrl,
                          MenuListIconClass = x.MenuListIconClass
                      });

                return MenuLists.ToList();
            }
            catch (Exception)
            {
                return null;
            }
        }

        public async Task<bool> UpdateMenuList(MenuListViewModel Model)
        {
            try
            {
                var Result = await _context.MenuLists.FirstOrDefaultAsync(a => a.MenuListId == Model.MenuListId);
                if (Result != null)
                {
                    Result.MenuListTitle = Model.MenuListTitle;
                    Result.MenuListDescription = Model.MenuListDescription;
                    Result.MenuListSortOrder = Model.MenuListSortOrder;
                    Result.MenuListParentId = Model.MenuListParentId;
                    Result.MenuListHasChildren = Model.MenuListHasChildren;
                    Result.MenuListNavigationUrl = Model.MenuListNavigationUrl;
                    Result.MenuListIconClass = Model.MenuListIconClass;

                    Result.UpdatedOn = DateTime.Now;
                    await _context.SaveChangesAsync();

                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
