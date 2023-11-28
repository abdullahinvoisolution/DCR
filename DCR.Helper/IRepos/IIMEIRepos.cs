
using DCR.Helper.ViewModel;


namespace DCR.ViewModel.IRepos
{
    public interface IIMEIRepos
    {
        Task<List<IMEIViewModel>> GetIMEIs();
        Task<IMEIViewModel> GetIMEI(int ImeiId);
        Task<bool> AddIMEI(IMEIViewModel Model);
        Task<bool> UpdateIMEI(IMEIViewModel Model);
        Task<bool> DeleteIMEI(int ImeiId);
    }
}
