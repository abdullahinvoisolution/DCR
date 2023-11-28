using DCR.ViewModel.ViewModel;


namespace DCR.ViewModel.IRepos
{
    public interface IReceiveRepos
    {
        Task<List<ReceiveViewModel>> GetReceives();
        Task<ReceiveViewModel> GetReceive(int ReceiveId);
        Task<bool> AddReceive(ReceiveViewModel Model);
        Task<bool> UpdateReceive(ReceiveViewModel Model);
        Task<bool> DeleteReceive(int ReceiveId);

    }
}
