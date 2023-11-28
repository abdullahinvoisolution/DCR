//using DAL.EntityModels;
//using DCR.ViewModel.IRepos;
//using DCR.ViewModel.ViewModel;


//namespace Repository.Repos
//{
//    public class MenuPermissionsAsignRepos : IMenuPermissionsAsignRepos
//    {
//        private readonly EMS_ITCContext _context;

//        public MenuPermissionsAsignRepos(EMS_ITCContext context)
//        {
//            _context = context;
//        }

//        public async Task<bool> AddMenuPermission(MenuPermissionAssignViewModel model)
//        {
//            try
//            {

//                if (model.RoleId != null && model.MenuListId != null)
//                {
//                    var MenuPermissionAssign = await _context.MenuPermissionAssigns.FirstOrDefaultAsync(m => m.RoleId == model.RoleId && m.MenuListId == model.MenuListId);
//                    // Create a corresponding warehouse record

//                    var menuPermissionAssign = new MenuPermissionAssign
//                    {
//                        RoleId = model.RoleId,
//                        MenuListId = model.MenuListId,
//                        // Map other properties as needed
//                    };
//                    if (model.RoleId == menuPermissionAssign.RoleId && model.MenuListId == menuPermissionAssign.MenuListId)
//                    {
//                        // Create a new User entity
//                        var NewMenuPermissionAssign = new MenuPermissionAssign
//                        {
//                            RoleId = model.RoleId,
//                            MenuListId = model.MenuListId,

//                        };

//                        _context.MenuPermissionAssigns.Add(NewMenuPermissionAssign);
//                        await _context.SaveChangesAsync();
//                    }
//                }
//                return true;
//            }
//            catch (Exception)
//            {
//                return false;
//            }
//        }

//        public async Task<bool> DeleteMenuPermission(int MenuAssignId)
//        {
//            var result = await _context.MenuPermissionAssigns.Where(m => m.MenuAssignId == MenuAssignId);
//            if (result != null)
//            {
//                //result.IsActive = false; // Mark the customer as inactive
//                await _context.SaveChangesAsync();
//                return result;
//            }
//            return true;
//        }

//        public async Task<MenuPermissionAssign> GetMenuPermission(int MenuAssignId)
//        {
//            return await _context.MenuPermissionAssigns.FirstOrDefaultAsync(m => m.Id == MenuAssignId);
//        }

//        public async Task<IEnumerable<MenuPermissionAssign>> GetMenuPermissions()
//        {
//            return await _context.MenuPermissionAssigns.ToListAsync();
//        }

//        public async Task<MenuPermissionAssignViewModel> UpdateMenuPermission(int MenuAssignId, MenuPermissionAssignViewModel model)
//        {
//            var result = await _context.MenuPermissionAssigns.FirstOrDefaultAsync(m => m.Id == MenuAssignId);
//            if (result != null)
//            {

//                result.RoleId = model.RoleId;
//                result.MenuListId = model.MenuListId;


//                //result.UpdatedOn = DateTime.Now; // Set the appropriate value
//                // Save changes to the database
//                await _context.SaveChangesAsync();

//                return model;

//            }
//            else
//            {
//                //  not found
//                return null;
//            }
//        }
//    }
//}
