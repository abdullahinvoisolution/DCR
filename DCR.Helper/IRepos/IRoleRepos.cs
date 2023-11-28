using DCR.Helper.ViewModel;


namespace Repository.IRepos
{
    public interface IRoleRepos
    {
        Task<List<RoleViewModel>> GetRoles();
        Task<RoleViewModel> GetRole(int RoleId);
        Task<bool> AddRole(RoleViewModel Model);
        Task<bool> UpdateRole(RoleViewModel Model);
        Task<bool> DeleteRole(int RoleId);

    }
}
