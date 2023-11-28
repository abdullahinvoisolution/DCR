using DAL.EntityModels;
using DCR.ViewModel.IRepos;
using DCR.ViewModel.ViewModel;
using Microsoft.EntityFrameworkCore;


namespace Repository.Repos
{
    public class InventoryRepos : IInventoryRepos
    {

        private readonly EMS_ITCContext _context;

        public InventoryRepos(EMS_ITCContext context)
        {
            _context = context;
        }

        public async Task<bool> AddInventory(InventoryViewModel Model)
        {
            try
            {
                var NewInventory = new Inventory
                {
                    ProductId = Model.ProductId,
                    TotalInventory = Model.TotalInventory,
                    ActiveInventory = Model.ActiveInventory,
                    InActiveInventory = Model.InActiveInventory,
                    InventoryDuration = Model.InventoryDuration
                };

                _context.Inventories.Add(NewInventory);
                await _context.SaveChangesAsync();

                return true;

            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<bool> DeleteInventory(int InventoryId)
        {
            try
            {
                var Result = await _context.Inventories.Where(a => a.InventoryId == InventoryId).FirstOrDefaultAsync();
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
        public async Task<InventoryViewModel> GetInventory(int InventoryId)
        {
            try
            {
                var Result = await _context.Inventories.FirstOrDefaultAsync(inv => inv.InventoryId == InventoryId && inv.IsActive == true);

                if (Result != null)
                {
                    var Model = new InventoryViewModel
                    {
                        ProductId = Result.ProductId,
                        TotalInventory = Result.TotalInventory,
                        ActiveInventory = Result.ActiveInventory,
                        InActiveInventory = Result.InActiveInventory,
                        InventoryDuration = Result.InventoryDuration
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

        public async Task<List<InventoryViewModel>> GetInventories()
        {
            try
            {
                InventoryViewModel Model = new InventoryViewModel();
                IEnumerable<InventoryViewModel> InventoryLists = _context.Inventories
                      .Where(inv => inv.IsActive == true)
                      .Select(inv => new InventoryViewModel
                      {
                          InventoryId = inv.InventoryId,
                          ProductId = inv.ProductId,
                          TotalInventory = inv.TotalInventory,
                          ActiveInventory = inv.ActiveInventory,
                          InActiveInventory = inv.InActiveInventory,
                          InventoryDuration = inv.InventoryDuration
                      });

                return InventoryLists.ToList();
            }
            catch (Exception)
            {
                return null;
            }
        }

        public async Task<bool> UpdateInventory(InventoryViewModel Model)
        {
            try
            {
                var Result = await _context.Inventories.FirstOrDefaultAsync(a => a.InventoryId == Model.InventoryId);
                if (Result != null)
                {

                    Result.ProductId = Model.ProductId;
                    Result.TotalInventory = Model.TotalInventory;
                    Result.ActiveInventory = Model.ActiveInventory;
                    Result.InActiveInventory = Model.InActiveInventory;
                    Result.InventoryDuration = Model.InventoryDuration;

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
