using DAL.EntityModels;
using DCR.ViewModel.IRepos;
using DCR.ViewModel.ViewModel;
using Microsoft.EntityFrameworkCore;

namespace Repository.Repos
{
    public class SaleOrderLineRepos : ISaleOrderLineRepos
    {
        private readonly EMS_ITCContext _context;


        public SaleOrderLineRepos(EMS_ITCContext context)
        {
            _context = context;

        }

        public async Task<bool> AddSaleOrderLine(SaleOrderLineViewModel Model)
        {
            try
            {
                var newSaleOrderLine = new SaleOrderLine
                {
                    ProductId = Model.ProductId,
                    SalesOrderId = Model.SalesOrderId,
                    Quantity = Model.Quantity,
                    Description = Model.Description,
                    UnitPrice = Model.UnitPrice,
                    Total = Model.Total,
                    IsActive = true

                };

                _context.SaleOrderLines.Add(newSaleOrderLine);
                await _context.SaveChangesAsync();

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<bool> DeleteSaleOrderline(int SaleOrderLineId)
        {

            try
            {
                var Result = await _context.SaleOrderLines.Where(s => s.SalesOrderLineId == SaleOrderLineId).FirstOrDefaultAsync();
                if (Result != null)
                {
                    Result.IsActive = false;
                    await _context.SaveChangesAsync();
                    return true;
                }
                else
                {
                    return true;
                }
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<SaleOrderLineViewModel> GetSaleOrderLine(int SaleOrderLineId)
        {
            try
            {
                var Result = await _context.SaleOrderLines.FirstOrDefaultAsync(s => s.SalesOrderLineId == SaleOrderLineId && s.IsActive == true);

                if (Result != null)
                {
                    var Model = new SaleOrderLineViewModel
                    {
                        ProductId = Result.ProductId,
                        SalesOrderId = Result.SalesOrderId,
                        Quantity = Result.Quantity,
                        Description = Result.Description,
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

        public async Task<List<SaleOrderLineViewModel>> GetSaleOrderLines()
        {
            try
            {
                SaleOrderLineViewModel Model = new SaleOrderLineViewModel();
                IEnumerable<SaleOrderLineViewModel> SalesOrderLists = _context.SaleOrderLines
                      .Where(s => s.IsActive == true)
                      .Select(s => new SaleOrderLineViewModel
                      {
                          SalesOrderLineId = s.SalesOrderLineId,
                          ProductId = s.ProductId,
                          SalesOrderId = s.SalesOrderId,
                          Quantity = s.Quantity,
                          Description = s.Description,
                          UnitPrice = s.UnitPrice,
                          Total = s.Total,
                      });

                return SalesOrderLists.ToList();
            }
            catch (Exception)
            {
                return null;
            }
        }

        public async Task<bool> UpdateSaleOrderLine(SaleOrderLineViewModel Model)
        {
            try
            {
                var Result = await _context.SaleOrderLines.FirstOrDefaultAsync(s => s.SalesOrderLineId == Model.SalesOrderLineId);
                if (Result != null)
                {

                    Result.ProductId = Model.ProductId;
                    Result.SalesOrderId = Model.SalesOrderId;
                    Result.Quantity = Model.Quantity;
                    Result.Description = Model.Description;
                    Result.UnitPrice = Model.UnitPrice;
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
            catch (Exception)
            {
                return false;
            }
        }
    }
}
