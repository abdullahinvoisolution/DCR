using DAL.EntityModels;
using DCR.Helper.ViewModel;
using DCR.ViewModel.IRepos;
using DCR.ViewModel.ViewModel;
using Microsoft.EntityFrameworkCore;


namespace Repository.Repos
{
    public class ProductRepos : IProductRepos
    {

        private readonly EMS_ITCContext _context;

        public ProductRepos(EMS_ITCContext context)
        {
            _context = context;
        }


        public async Task<bool> AddProduct(ProductViewModel model)
        {
            try
            {
                // Create a new User entity
                var NewProduct = new Product
                {
                    ProductType = model.ProductType,
                    ProductQuantity = model.ProductQuantity,
                    ProductPrice = model.ProductPrice,
                    ProductSku = model.ProductSku,
                    ProductCode = model.ProductCode,
                    MarketName = model.MarketName,
                    Brand = model.Brand,
                    Memory = model.Memory,
                    Model = model.Model,
                    Color = model.Color,
                    Series = model.Series

                };
                NewProduct.CreatedBy = "Admin";

                _context.Products.Add(NewProduct);
                await _context.SaveChangesAsync();


              

                return true;
            }
            catch (Exception)
            {

                return false;
            }
        }

    public async Task<bool> DeleteProduct(int ProductId)
    {
            var result = await _context.Products.Where(a => a.ProductId == ProductId).FirstOrDefaultAsync();
            if (result != null)
            {
                result.IsActive = false; // Mark the customer as inactive
                await _context.SaveChangesAsync();
                return true;
            }
            return false;
        }

    public async Task<ProductViewModel> GetProduct(int ProductId)
    {
            var result = await _context.Products.FirstOrDefaultAsync(a => a.ProductId == ProductId && a.IsActive == true);


            if (result != null)
            {
                var productViewModel = new ProductViewModel
                {
                    ProductId = result.ProductId,
                    ProductType = result.ProductType,
                    MaterialId = result.MaterialId,
                    ProductQuantity = result.ProductQuantity,
                    ProductPrice = result.ProductPrice,
                    ProductSku = result.ProductSku,
                    ProductCode = result.ProductCode,
                    MarketName = result.MarketName,
                    Brand = result.Brand,
                    Memory = result.Memory,
                    Model = result.Model,
                    Color = result.Color,
                    Series = result.Series,
                };

                return productViewModel;
            }
            else
            {
                return null;
            }
        }

    public async Task<List<ProductViewModel>> GetProducts(DataTableViewModel model)
    {
            IEnumerable<ProductViewModel> products = _context.Products
                 .Where(x => x.IsActive == true)
                 .Select(x => new ProductViewModel
                 {

                     ProductType = x.ProductType,
                     MaterialId = x.MaterialId,
                     ProductQuantity = x.ProductQuantity,
                     ProductPrice = x.ProductPrice,
                     ProductSku = x.ProductSku,
                     ProductCode = x.ProductCode,
                     MarketName = x.MarketName,
                     Brand = x.Brand,
                     Memory = x.Memory,
                     Model = x.Model,
                     Color = x.Color,
                     Series = x.Series,
                 });

            if (products != null && products.Any())
            {
                string sort = string.Empty;

                if (sort == model.sortColum)
                {
                    if (sort.Contains(model.sortColumDir))
                    {
                        products = products.OrderBy(x => x.ProductId);
                    }
                    else
                    {
                        products = products.OrderByDescending(x => x.ProductId);

                    }
                }

                var _branch = products.Skip(model.skip).Take(model.length
                    ).ToList();
                return _branch;
            }
            else
            {
                // Handle the case where no active branches are found
                return new List<ProductViewModel>(); // Returning an empty list
            }
        }

    public async Task<bool> UpdateProduct(ProductViewModel model)
    {
            var result = await _context.Products.FirstOrDefaultAsync(a => a.ProductId == model.ProductId);
            if (result != null)
            {

                result.ProductType = model.ProductType;
                result.ProductQuantity = model.ProductQuantity;
                result.ProductPrice = model.ProductPrice;
                result.ProductSku = model.ProductSku;
                result.ProductCode = model.ProductCode;
                result.MarketName = model.MarketName;
                result.Brand = model.Brand;
                result.Memory = model.Memory;
                result.Model = model.Model;
                result.Color = model.Color;
                result.Series = model.Series;

                 // Update other fields if needed, e.g., UpdatedBy and UpdatedOn
                //result.UpdatedBy = model.Name; // Set the appropriate value

                result.UpdatedOn = DateTime.Now; // Set the appropriate value
                // Save changes to the database
                await _context.SaveChangesAsync();

                return true;
            }
            else
            {
                // User not found
                return false;
            }
        }
}
}
