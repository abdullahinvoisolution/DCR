using DAL.EntityModels;
using DCR.Helper.ViewModel;
using DCR.ViewModel.IRepos;
using DCR.ViewModel.ViewModel;
using Microsoft.EntityFrameworkCore;


namespace Repository.Repos
{
    public class PurchaseOrderRepos : IPurchaseOrderRepos
    {

        private readonly EMS_ITCContext _context;

        public PurchaseOrderRepos(EMS_ITCContext context)
        {
            _context = context;
        }


        public async Task<bool> AddPurchaseOrder(PurchaseOrderViewModel Model)
        {
            try
            {

                var NewPurchaseOrder = new PurchaseOrder
                {
                    VendorId = Model.VendorId,
                    PurchaseName = Model.PurchaseName,
                    PurchaseQuantity = Model.PurchaseQuantity,
                    PurchaseDescription = Model.PurchaseDescription,
                    DateOfOrder = Model.DateOfOrder,
                    DateOfDelivery = Model.DateOfDelivery,
                    Amount = Model.Amount,
                    SubTotal = Model.SubTotal,
                    Discount = Model.Discount,
                    Total = Model.Total,
                    IsActive = true
                };

                _context.PurchaseOrders.Add(NewPurchaseOrder);
                await _context.SaveChangesAsync();

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<bool> DeletePurchaseOrder(int PurchaseId)
        {
            try
            {
                var Result = await _context.PurchaseOrders.Where(po => po.PurchaseId == PurchaseId).FirstOrDefaultAsync();
                if (Result != null)
                {
                    Result.IsActive = false;
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

        public async Task<PurchaseOrderViewModel> GetPurchaseOrder(int PurchaseId)
        {
            try
            {
                var Result = await _context.PurchaseOrders.FirstOrDefaultAsync(po => po.PurchaseId == PurchaseId && po.IsActive == true);

                if (Result != null)
                {
                    var Model = new PurchaseOrderViewModel
                    {
                        VendorId = Result.VendorId,
                        PurchaseName = Result.PurchaseName,
                        PurchaseQuantity = Result.PurchaseQuantity,
                        PurchaseDescription = Result.PurchaseDescription,
                        DateOfOrder = Result.DateOfOrder,
                        DateOfDelivery = Result.DateOfDelivery,
                        Amount = Result.Amount,
                        SubTotal = Result.SubTotal,
                        Discount = Result.Discount,
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

        public async Task<List<PurchaseOrderViewModel>> GetPurchaseOrders()
        {
            try
            {
                PurchaseOrderViewModel Model = new PurchaseOrderViewModel();
                IEnumerable<PurchaseOrderViewModel> PurchaseOrderLists = _context.PurchaseOrders
                      .Where(po => po.IsActive == true)
                      .Select(po => new PurchaseOrderViewModel
                      {
                          PurchaseId = po.PurchaseId,
                          VendorId = po.VendorId,
                          PurchaseName = po.PurchaseName,
                          PurchaseQuantity = po.PurchaseQuantity,
                          PurchaseDescription = po.PurchaseDescription,
                          DateOfOrder = po.DateOfOrder,
                          DateOfDelivery = po.DateOfDelivery,
                          Amount = po.Amount,
                          SubTotal = po.SubTotal,
                          Discount = po.Discount,
                          Total = po.Total
                      });

                return PurchaseOrderLists.ToList();
            }
            catch (Exception)
            {
                return null;
            }
        }

        public async Task<bool> UpdatePurchaseOrder(PurchaseOrderViewModel Model)
        {
            var Result = await _context.PurchaseOrders.FirstOrDefaultAsync(po => po.PurchaseId == Model.PurchaseId);
            if (Result != null)
            {
                Result.PurchaseName = Model.PurchaseName;
                Result.PurchaseQuantity = Model.PurchaseQuantity;
                Result.PurchaseDescription = Model.PurchaseDescription;
                Result.DateOfOrder = Model.DateOfOrder;
                Result.DateOfDelivery = Model.DateOfDelivery;
                Result.Amount = Model.Amount;
                Result.SubTotal = Model.SubTotal;
                Result.Discount = Model.Discount;
                Result.Total = Model.Total;

                Result.UpdatedOn = DateTime.Now; 
                await _context.SaveChangesAsync();

                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
