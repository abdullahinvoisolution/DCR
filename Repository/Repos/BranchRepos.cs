using DAL.EntityModels;
using DCR.Helper.ViewModel;
using DCR.ViewModel.IRepos;
using DCR.ViewModel.ViewModel;
using Microsoft.EntityFrameworkCore;

namespace Repository.Repos
{
    public class BranchRepos : IBranchRepos
    {
        private readonly EMS_ITCContext _context;

        public BranchRepos(EMS_ITCContext context)
        {
            _context = context;
        }
        public async Task<bool> AddBranch(BranchViewModel model)
        {
            try
            {
                // Create a new User entity
                var newBranch = new Branch
                {
                    BranchName = model.Name,
                    BranchPerson = model.Person,
                    BranchEmail = model.Email,
                    BranchPhone = model.Phone,
                    BranchAddress = model.Address,
                    Country = model.Country,
                    BranchCity = model.City,
                    BranchState = model.State,
                    BranchPostalCode = model.PostalCode,
                    CreatedBy = "Admin"
                };
               
                _context.Branches.Add(newBranch);
                await _context.SaveChangesAsync();

                return true;
            }
            catch (Exception)
            {

                return false;
            }
        }

        public async Task<bool> DeleteBranch(int BranchId)
        {
            var result = await _context.Branches.Where(a => a.BranchId == BranchId).FirstOrDefaultAsync();
            if (result != null)
            {
                var branch = result;
                branch.IsActive = false;
                await _context.SaveChangesAsync();
                return true;
            }
            return false;
        }

        public async Task<BranchViewModel> GetBranch(int BranchId)
        {
            var result = await _context.Branches.FirstOrDefaultAsync(a => a.BranchId == BranchId && a.IsActive == true);

            if (result != null)
            {
                var branchViewModel = new BranchViewModel
                {
                   Name  = result.BranchName,
                   Person = result.BranchPerson,
                   Email = result.BranchEmail,
                   Phone = result.BranchPhone,
                   Address = result.BranchAddress,
                   Country = result.Country,
                   City = result.BranchCity,
                   State = result.BranchState,
                   PostalCode = result.BranchPostalCode
                };

                return branchViewModel;
            }
            else
            {
                return null;
            }
        }
        public async Task<List<BranchViewModel>> GetBranches(DataTableViewModel model)
        {
            IEnumerable<BranchViewModel> branches = _context.Branches
                .Where(x => x.IsActive == true)
                .Select(x => new BranchViewModel
                {
                    Name = x.BranchName,
                    Person = x.BranchPerson,
                    Email = x.BranchEmail,
                    Phone = x.BranchPhone,
                    Address = x.BranchAddress,
                    Country = x.Country,
                    City = x.BranchCity,
                    State = x.BranchState,
                    PostalCode = x.BranchPostalCode
                });

            if (branches != null && branches.Any())
            {
                string sort = string.Empty;
                
                if (sort == model.sortColum)
                {
                    if (sort.Contains(model.sortColumDir))
                    {
                        branches = branches.OrderBy(x => x.Name);
                    }
                    else
                    {
                        branches = branches.OrderByDescending(x => x.Name);

                    }
                }

               var _branch = branches.Skip(model.skip).Take(model.length
                   ).ToList();
                return _branch;
            }
            else
            {
                // Handle the case where no active branches are found
                return new List<BranchViewModel>(); // Returning an empty list
            }
        }

        public async Task<bool> UpdateBranch(BranchViewModel model)
        {
            var result = await _context.Branches.FirstOrDefaultAsync(a => a.BranchId == model.BranchId);
            if (result != null)
            {

                result.BranchName = model.Name;
                result.BranchPerson = model.Person;
                result.BranchEmail = model.Email;
                result.BranchPhone = model.Phone;
                result.BranchAddress = model.Address;
                result.Country = model.Country;
                result.BranchCity = model.City;
                result.BranchState = model.State;
                result.BranchPostalCode = model.PostalCode;
                result.UpdatedOn = DateTime.Now; 

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
