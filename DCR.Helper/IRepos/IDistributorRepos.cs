using DCR.Helper.ViewModel;


namespace DCR.ViewModel.IRepos
{
    public interface IDistributorRepos
    {
        Task<List<DistributorViewModel>> GetDistributors();
        Task<DistributorViewModel> GetDistributor(int DistributorId);
        Task<bool> AddDistributor(DistributorViewModel Model);
        Task<bool> UpdateDistributor(DistributorViewModel Model);
        Task<bool> DeleteDistributor(int DistributorId);

    }
}
