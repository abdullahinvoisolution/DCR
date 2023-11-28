using DCR.ViewModel.ViewModel;

namespace DCR.ViewModel.IRepos
{
    public interface IMenuListRepos
    {
        Task<List<MenuListViewModel>> GetMenuLists();
        Task<MenuListViewModel> GetMenuList(int MenuId);
        Task<bool> AddMenuList(MenuListViewModel Model);
        Task<bool> UpdateMenuList(MenuListViewModel Model);
        Task<bool> DeleteMenuList(int MenuId);
    }
}
