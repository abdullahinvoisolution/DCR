using DCR.Helper.ViewModel;


namespace DCR.ViewModel.IRepos
{
    public interface IPurchaseOrderRepos
    {
        Task<List<PurchaseOrderViewModel>> GetPurchaseOrders();
        Task<PurchaseOrderViewModel> GetPurchaseOrder(int PurchaseId);
        Task<bool> AddPurchaseOrder(PurchaseOrderViewModel Model);
        Task<bool> UpdatePurchaseOrder(PurchaseOrderViewModel Model);
        Task<bool> DeletePurchaseOrder(int PurchaseId);
    }
}
