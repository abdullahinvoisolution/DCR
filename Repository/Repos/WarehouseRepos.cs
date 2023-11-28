using DAL.EntityModels;
using DCR.Helper.ViewModel;
using DCR.ViewModel.IRepos;
using Microsoft.EntityFrameworkCore;

namespace Repository
{
    public class WarehouseRepos : IWarehouseRepos
    {

        private readonly EMS_ITCContext _context;

        public WarehouseRepos(EMS_ITCContext context)
        {
            _context = context;
        }

        public async Task<WarehouseViewModel> AddWarehouse(WarehouseViewModel model)
        {
            try
            {
                if (model.BranchId != null)
                {
                    var branch = await _context.Branches.FirstOrDefaultAsync(b => b.BranchId == model.BranchId && b.IsActive == true);
                    // Create a corresponding warehouse record

                    var warehouse = new Warehouse
                    {
                        BranchId = branch.BranchId,
                        // Map other properties as needed
                    };
                    if (model.BranchId == warehouse.BranchId)
                    {
                        // Create a new User entity
                        var NewWarehouse = new Warehouse
                        {
                            BranchId = model.BranchId,
                            WarehouseName = model.Name,
                            WarehouseDescription = model.Description,
                            WarehouseType = model.Type,
                            CreatedBy = "Admin"
                        };

                        _context.Warehouses.Add(NewWarehouse);
                        await _context.SaveChangesAsync();
                    }
                }
                 
                    return model;

                }
            catch (Exception)
            {

                return null;
            }
        
        }

        public async Task<object> DeleteWarehouse(int WarehouseId)
        {
            var result = await _context.Warehouses.Where(a => a.WarehouseId == WarehouseId).FirstOrDefaultAsync();
            if (result != null)
            {
                result.IsActive = false; // Mark the customer as inactive
                await _context.SaveChangesAsync();
                return result;
            }
            return null;
        }

        public async Task<object> GetWarehouse(int WarehouseId)
        {
            return await _context.Warehouses.FirstOrDefaultAsync(a => a.WarehouseId == WarehouseId && a.IsActive == true);
        }

        public async Task<IEnumerable<object>> GetWarehouses()
        {
            return await _context.Warehouses.Where(x => x.IsActive == true).ToListAsync();
        }

        public async Task<WarehouseViewModel> UpdateWarehouse(int WarehouseId, WarehouseViewModel model)
        {
            var result = await _context.Warehouses.FirstOrDefaultAsync(a => a.WarehouseId == WarehouseId);
            if (result != null)
            {

                result.WarehouseName = model.Name;
                result.WarehouseDescription = model.Description;
                result.WarehouseType = model.Type;


                // Update other fields if needed, e.g., UpdatedBy and UpdatedOn
                //result.UpdatedBy = model.Name; // Set the appropriate value

                result.UpdatedOn = DateTime.Now; // Set the appropriate value
                // Save changes to the database
                await _context.SaveChangesAsync();

                return model;
            }
            else
            {
                // User not found
                return null;
            }
        }
    }
}
