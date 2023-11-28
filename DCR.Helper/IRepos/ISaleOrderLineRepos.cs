using DCR.ViewModel.ViewModel;


namespace DCR.ViewModel.IRepos
{
    public interface ISaleOrderLineRepos
    {
        Task<List<SaleOrderLineViewModel>> GetSaleOrderLines();
        Task<SaleOrderLineViewModel> GetSaleOrderLine(int SaleOrderLineId);
        Task<bool> AddSaleOrderLine(SaleOrderLineViewModel Model);
        Task<bool> UpdateSaleOrderLine(SaleOrderLineViewModel Model);
        Task<bool> DeleteSaleOrderline(int SaleOrderLineId);
    }
}
