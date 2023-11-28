using DCR.Helper.ViewModel;

namespace DCR.ViewModel.IRepos
{
    public interface IAccountRepos
    {
        Task<List<LoginViewModel>> GetUsers();
        Task<LoginViewModel> GetUser(string UserLoginId);
        Task<bool> LoginUser(PasswordUpdateViewModel model);
        Task<bool> AddUser(LoginViewModel model);
        Task<bool> UpdateUserPassword(PasswordUpdateViewModel model);
        Task<string> GetUserEmail(string UserLoginId);
        Task<string> GetUserPhoneNumber(string UserLoginId);
        Task<bool> DeleteUser(string UserLoginId);
    }
}
