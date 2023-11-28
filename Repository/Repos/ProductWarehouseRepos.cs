using DAL.EntityModels;
using DCR.ViewModel.IRepos;
using DCR.ViewModel.ViewModel;
using Microsoft.EntityFrameworkCore;


namespace Repository.Repos
{
    public class ProductWarehouseRepos : IProductWarehouseRepos
    {
        private readonly EMS_ITCContext _context;

        public ProductWarehouseRepos(EMS_ITCContext context)
        {
            _context = context;
        }

        public async Task<bool> AddProductWarehouse(ProductWarehouseViewModel Model)
        {
            try
            {
                var Newproductwarehouse = new ProductWarehouse
                {
                    ProductId = Model.ProductId,
                    WarehouseId = Model.WarehouseId

                };

                _context.ProductWarehouses.Add(Newproductwarehouse);
                await _context.SaveChangesAsync();


                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<bool> DeleteProductWarehouse(int ProductWarehouseId)
        {
            try
            {
                var Result = await _context.ProductWarehouses.Where(pw => pw.ProductWarehouseId == ProductWarehouseId).FirstOrDefaultAsync();
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

        public async Task<ProductWarehouseViewModel> GetProductWarehouse(int ProductWarehouseId)
        {
            try
            {
                var Result = await _context.ProductWarehouses.FirstOrDefaultAsync(pw => pw.ProductWarehouseId == ProductWarehouseId);

                if (Result != null)
                {
                    var Model = new ProductWarehouseViewModel
                    {
                        ProductId = Result.ProductId,
                        WarehouseId = Result.WarehouseId
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

        public async Task<List<ProductWarehouseViewModel>> GetProductWarehouses()
        {
            try
            {
                ProductWarehouseViewModel Model = new ProductWarehouseViewModel();
                IEnumerable<ProductWarehouseViewModel> ProductWarehouseLists = _context.ProductWarehouses
                    .Select(pw => new ProductWarehouseViewModel
                    {
                        ProductWarehouseId = pw.ProductWarehouseId,
                        ProductId = pw.ProductId,
                        WarehouseId = pw.WarehouseId
                    });

                return ProductWarehouseLists.ToList();
            }
            catch (Exception)
            {
                return null;
            }
        }

        public async Task<bool> UpdateProductWarehouse(ProductWarehouseViewModel Model)
        {
            try
            {
                var Result = await _context.ProductWarehouses.FirstOrDefaultAsync(a => a.ProductWarehouseId == Model.ProductWarehouseId);
                if (Result != null)
                {

                    Result.ProductId = Model.ProductId;
                    Result.WarehouseId = Model.WarehouseId;

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
