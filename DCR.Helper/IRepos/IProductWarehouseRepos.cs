using DCR.ViewModel.ViewModel;


namespace DCR.ViewModel.IRepos
{
    public interface IProductWarehouseRepos
    {
        Task<List<ProductWarehouseViewModel>> GetProductWarehouses();
        Task<ProductWarehouseViewModel> GetProductWarehouse(int ProductWarehouseId);
        Task<bool> AddProductWarehouse(ProductWarehouseViewModel Model);
        Task<bool> UpdateProductWarehouse(ProductWarehouseViewModel Model);
        Task<bool> DeleteProductWarehouse(int ProductWarehouseId);
    }
}
