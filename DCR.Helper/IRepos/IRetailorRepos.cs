using DCR.Helper.ViewModel;


namespace DCR.ViewModel.IRepos
{
    public interface IRetailorRepos
    {
        Task<List<RetailerViewModel>> GetRetailers();
        Task<RetailerViewModel> GetRetailer(int RetailerId);
        Task<bool> AddRetailer(RetailerViewModel Model);
        Task<bool> UpdateRetailer(RetailerViewModel Model);
        Task<bool> DeleteRetailer(int RetailerId);
    }
}
