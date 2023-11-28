using DCR.Helper.ViewModel;
using DCR.ViewModel.ViewModel;

namespace DCR.ViewModel.IRepos
{
    public interface IProductRepos
    {
        Task<List<ProductViewModel>> GetProducts(DataTableViewModel model);
        Task<ProductViewModel> GetProduct(int ProductId);
        Task<bool> AddProduct(ProductViewModel model);
        Task<bool> UpdateProduct(ProductViewModel model);
        Task<bool> DeleteProduct(int ProductId);
    }
}
