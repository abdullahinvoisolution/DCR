using DAL.EntityModels;
using DCR.ViewModel.IRepos;
using DCR.ViewModel.ViewModel;
using Microsoft.EntityFrameworkCore;


namespace Repository.Repos
{
    public class PurchaseOrderLineRepos : IPurchaseOrderline
    {
        private readonly EMS_ITCContext _context;

        public PurchaseOrderLineRepos(EMS_ITCContext context)
        {
            _context = context;
        }
        public async Task<bool> AddPurchaseOrderLine(PurchaseOrderLineViewModel Model)
        {
            try
            {
                var newPurchaseOrderLine = new PurchaseOrderLine
                {
                    PurchaseId = Model.PurchaseId,
                    OrderLineDescription = Model.OrderLineDescription,
                    OrderLineQuantity = Model.OrderLineQuantity,
                    OrderLineUnitPrice = Model.OrderLineUnitPrice,
                    TaxPercentage = Model.TaxPercentage,
                    TotalPrice = Model.TotalPrice,
                    IsActive = true
                };

                _context.PurchaseOrderLines.Add(newPurchaseOrderLine);
                await _context.SaveChangesAsync();

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<bool> DeletePurchaseOrderLine(int PurchaseOrderLineId)
        {
            try
            {
                var Result = await _context.PurchaseOrderLines.Where(pol => pol.OrderLineId == PurchaseOrderLineId).FirstOrDefaultAsync();
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

        public async Task<PurchaseOrderLineViewModel> GetPurchaseOrderLine(int PurchaseOrderLineId)
        {
            try
            {
                var Result = await _context.PurchaseOrderLines.FirstOrDefaultAsync(pol => pol.OrderLineId == PurchaseOrderLineId && pol.IsActive == true);

                if (Result != null)
                {
                    var Model = new PurchaseOrderLineViewModel
                    {
                        PurchaseId = Result.PurchaseId,
                        OrderLineDescription = Result.OrderLineDescription,
                        OrderLineQuantity = Result.OrderLineQuantity,
                        OrderLineUnitPrice = Result.OrderLineUnitPrice,
                        TaxPercentage = Result.TaxPercentage,
                        TotalPrice = Result.TotalPrice
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

        public async Task<List<PurchaseOrderLineViewModel>> GetPurchaseOrderLines()
        {
            try
            {
                PurchaseOrderLineViewModel Model = new PurchaseOrderLineViewModel();
                IEnumerable<PurchaseOrderLineViewModel> PurchaseOrderLineLists = _context.PurchaseOrderLines
                      .Where(pol => pol.IsActive == true)
                      .Select(pol => new PurchaseOrderLineViewModel
                      {
                          OrderLineId = pol.OrderLineId,
                          PurchaseId = pol.PurchaseId,
                          OrderLineDescription = pol.OrderLineDescription,
                          OrderLineQuantity = pol.OrderLineQuantity,
                          OrderLineUnitPrice = pol.OrderLineUnitPrice,
                          TaxPercentage = pol.TaxPercentage,
                          TotalPrice = pol.TotalPrice
                      });

                return PurchaseOrderLineLists.ToList();
            }
            catch (Exception)
            {
                return null;
            }
        }

        public async Task<bool> UpdatePurchaseOrderLine(PurchaseOrderLineViewModel Model)
        {
            try
            {
                var Result = await _context.PurchaseOrderLines.FirstOrDefaultAsync(pol => pol.OrderLineId == Model.OrderLineId);
                if (Result != null)
                {

                    Result.OrderLineDescription = Model.OrderLineDescription;
                    Result.OrderLineQuantity = Model.OrderLineQuantity;
                    Result.OrderLineUnitPrice = Model.OrderLineUnitPrice;
                    Result.TaxPercentage = Model.TaxPercentage;
                    Result.TotalPrice = Model.TotalPrice;

                    Result.UpdatedOn = DateTime.Now;
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
