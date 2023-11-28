using DAL.EntityModels;
using DCR.ViewModel.IRepos;
using DCR.ViewModel.ViewModel;
using Microsoft.EntityFrameworkCore;


namespace Repository.Repos
{
    public class PurchaseOrderDetailRepos : IPurchaseOrderDetailRepos
    {

        private readonly EMS_ITCContext _context;

        public PurchaseOrderDetailRepos(EMS_ITCContext context)
        {
            _context = context;
        }

        public async Task<bool> AddPurchaseOrderDetail(PurchaseOrderDetailViewModel Model)
        {
            try
            {
                var newPurchaseOrderDetail = new PurchaseOrderDetail
                {
                    ProductId = Model.ProductId,
                    Quantity = Model.Quantity,
                    UnitPrice = Model.UnitPrice,
                    Total = Model.Total
                };

                _context.PurchaseOrderDetails.Add(newPurchaseOrderDetail);
                await _context.SaveChangesAsync();

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<bool> DeletePurchaseOrderDetail(int PurchaseOrderDetailId)
        {
            try
            {
                var Result = await _context.PurchaseOrderDetails.Where(pod => pod.PurchaseOrderDetailId == PurchaseOrderDetailId).FirstOrDefaultAsync();
                if (Result != null)
                {
                    await _context.SaveChangesAsync();
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<PurchaseOrderDetailViewModel> GetPurchaseOrderDetail(int PurchaseOrderDetailId)
        {
            try
            {
                var Result = await _context.PurchaseOrderDetails.FirstOrDefaultAsync(pod => pod.PurchaseOrderDetailId == PurchaseOrderDetailId);

                if (Result != null)
                {
                    var Model = new PurchaseOrderDetailViewModel
                    {
                        ProductId = Result.ProductId,
                        Quantity = Result.Quantity,
                        UnitPrice = Result.UnitPrice,
                        Total = Result.Total 
                    };

                    return Model;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception)
            {

                return null;
            }
        }

        public async Task<List<PurchaseOrderDetailViewModel>> GetPurchaseOrderDetails()
        {
            try
            {
                PurchaseOrderDetailViewModel Model = new PurchaseOrderDetailViewModel();
                IEnumerable<PurchaseOrderDetailViewModel> PurchaseOrderDetailLists = _context.PurchaseOrderDetails
                      .Select(pod => new PurchaseOrderDetailViewModel
                      {
                          PurchaseOrderDetailId = pod.PurchaseOrderDetailId,
                          ProductId = pod.ProductId,
                          Quantity = pod.Quantity,
                          UnitPrice = pod.UnitPrice,
                          Total = pod.Total
                      });

                return PurchaseOrderDetailLists.ToList();
            }
            catch (Exception)
            {
                return null;
            }
        }

        public async Task<bool> UpdatePurchaseOrderDetail(PurchaseOrderDetailViewModel Model)
        {
            try
            {
                var Result = await _context.PurchaseOrderDetails.FirstOrDefaultAsync(pod => pod.PurchaseOrderDetailId == Model.PurchaseOrderDetailId);
                if (Result != null)
                {

                    Result.Quantity = Model.Quantity;
                    Result.UnitPrice = Model.UnitPrice;
                    Result.Total = Model.Total;

                    await _context.SaveChangesAsync();

                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
