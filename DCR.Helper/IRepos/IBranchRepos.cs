using DCR.Helper.ViewModel;
using DCR.ViewModel.ViewModel;

namespace DCR.ViewModel.IRepos
{
    public interface IBranchRepos
    {
        Task<List<BranchViewModel>> GetBranches(DataTableViewModel model);
        Task<BranchViewModel> GetBranch(int BranchId);
        Task<bool>DeleteBranch(int BranchId);
        Task<bool> AddBranch(BranchViewModel model);
        Task<bool> UpdateBranch(BranchViewModel model);
    }
}
