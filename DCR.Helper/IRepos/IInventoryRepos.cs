using DCR.ViewModel.ViewModel;


namespace DCR.ViewModel.IRepos
{
    public interface IInventoryRepos
    {
        Task<List<InventoryViewModel>> GetInventories();
        Task<InventoryViewModel> GetInventory(int InventoryId);
        Task<bool> AddInventory(InventoryViewModel Model);
        Task<bool> UpdateInventory(InventoryViewModel Model);
        Task<bool> DeleteInventory(int InventoryId);
    }
}
