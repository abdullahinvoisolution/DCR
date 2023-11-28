
using DCR.ViewModel.ViewModel;


namespace DCR.ViewModel.IRepos
{
    public interface IDistributorIMEIRepos
    {
        Task<List<DistibutorIMEIViewModel>> GetDistributorImeis();
        Task<DistibutorIMEIViewModel> GetDistributorImei(int DistributorImeiId);
        Task<bool> AddDistributorImei(DistibutorIMEIViewModel Model);
        Task<bool> UpdateDistributorImei(DistibutorIMEIViewModel Model);
        Task<bool> DeleteDistributorImei(int DistributorImeiId);
    }
}
