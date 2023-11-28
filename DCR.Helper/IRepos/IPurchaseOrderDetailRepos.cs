using DCR.ViewModel.ViewModel;

namespace DCR.ViewModel.IRepos
{
    public interface IPurchaseOrderDetailRepos
    {
        Task<List<PurchaseOrderDetailViewModel>> GetPurchaseOrderDetails();
        Task<PurchaseOrderDetailViewModel> GetPurchaseOrderDetail(int PurchaseOrderDetailId);
        Task<bool> AddPurchaseOrderDetail(PurchaseOrderDetailViewModel Model);
        Task<bool> UpdatePurchaseOrderDetail(PurchaseOrderDetailViewModel Model);
        Task<bool> DeletePurchaseOrderDetail(int PurchaseOrderDetailId);
    }
}
