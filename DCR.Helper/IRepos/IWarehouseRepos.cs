using DCR.Helper.ViewModel;


namespace DCR.ViewModel.IRepos
{
    public interface IWarehouseRepos
    {
        Task<IEnumerable<object>> GetWarehouses();
        Task<object> GetWarehouse(int WarehouseId);
        Task<WarehouseViewModel> AddWarehouse(WarehouseViewModel model);
        Task<WarehouseViewModel> UpdateWarehouse(int WarehouseId, WarehouseViewModel model);
        Task<object> DeleteWarehouse(int WarehouseId);
    }
}
