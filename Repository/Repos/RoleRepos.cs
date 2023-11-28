using DAL.EntityModels;
using DCR.Helper.ViewModel;
using DCR.ViewModel.ViewModel;
using Microsoft.EntityFrameworkCore;
using Repository.IRepos;


namespace Repository.Repos
{
    public class RoleRepos : IRoleRepos
    {
        private readonly EMS_ITCContext _context;

        public RoleRepos(EMS_ITCContext context)
        {

            _context = context;

        }

        public async Task<bool> AddRole(RoleViewModel Model)
        {
            try
            {
                var NewRole = new Role
                {
                    RoleName = Model.RoleName,
                    RoleDescription = Model.RoleDescription,
                    CreatedBy = "Admin"

                };

                _context.Roles.Add(NewRole);
                await _context.SaveChangesAsync();


                return true;
            }
            catch (Exception)
            {

                return false;
            }
        }

        public async Task<bool> DeleteRole(int RoleId)
        {
            try
            {
                var Result = await _context.Roles.Where(r => r.RoleId == RoleId).FirstOrDefaultAsync();
                if (Result != null)
                {
                    Result.IsActive = false;
                    await _context.SaveChangesAsync();
                    return true;
                }
                return false;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<RoleViewModel> GetRole(int RoleId)
        {
            try
            {
                var Result = await _context.Roles.FirstOrDefaultAsync(r => r.RoleId == RoleId && r.IsActive == true);

                if (Result != null)
                {
                    var Model = new RoleViewModel
                    {
                        RoleName = Result.RoleName,
                        RoleDescription = Result.RoleDescription
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

        public async Task<List<RoleViewModel>> GetRoles()
        {
            try
            {
                RoleViewModel Model = new RoleViewModel();
                IEnumerable<RoleViewModel> RoleLists = _context.Roles
                      .Where(r => r.IsActive == true)
                      .Select(r => new RoleViewModel
                      {
                          RoleId = r.RoleId,
                          RoleName = r.RoleName,
                          RoleDescription = r.RoleDescription,
                      });

                return RoleLists.ToList();
            }
            catch (Exception)
            {
                return null;
            }
        }

        public async Task<bool> UpdateRole(RoleViewModel Model)
        {
            try
            {
                var Result = await _context.Roles.FirstOrDefaultAsync(a => a.RoleId == Model.RoleId);
                if (Result != null)
                {

                    Result.RoleName = Model.RoleName;
                    Result.RoleDescription = Model.RoleDescription;

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
