using DCR.ViewModel.ViewModel;


namespace DCR.ViewModel.IRepos
{
    public interface IPurchaseOrderline
    {
        Task<List<PurchaseOrderLineViewModel>> GetPurchaseOrderLines();
        Task<PurchaseOrderLineViewModel> GetPurchaseOrderLine(int PurchaseOrderLineId);
        Task<bool> AddPurchaseOrderLine(PurchaseOrderLineViewModel Model);
        Task<bool> UpdatePurchaseOrderLine(PurchaseOrderLineViewModel Model);
        Task<bool> DeletePurchaseOrderLine(int PurchaseOrderLineId);
    }
}
