using DAL.EntityModels;
using DCR.Helper.ViewModel;
using DCR.ViewModel.IRepos;
using DCR.ViewModel.ViewModel;
using Microsoft.EntityFrameworkCore;

namespace Repository.Repos
{
    public class DistributorRepos : IDistributorRepos
    {
        private readonly EMS_ITCContext _context;

        public DistributorRepos(EMS_ITCContext context)
        {
            _context = context;
        }
        public async Task<bool> AddDistributor(DistributorViewModel Model)
        {
            try
            {
                var NewDistributor = new Distributor
                {
                    DistributorName = Model.DistributorName,
                    DistributorType = Model.DistributorType,
                    DistributorEmail = Model.DistributorEmail,
                    DistributorPhone = Model.DistributorPhone,
                    DistributorAddress = Model.DistributorAddress,
                    IsActive = true,
                };

                _context.Distributors.Add(NewDistributor);
                await _context.SaveChangesAsync();

                return true;
            }
            catch (Exception)
            {

                return false;
            }
        }

        public async Task<bool> DeleteDistributor(int DistributorId)
        {
            try
            {
                var Result = await _context.Distributors.Where(d => d.DistributorId == DistributorId).FirstOrDefaultAsync();
                if (Result != null)
                {
                    Result.IsActive = false; // Mark the customer as inactive
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

        public async Task<DistributorViewModel> GetDistributor(int DistributorId)
        {
            try
            {
                var Result = await _context.Distributors.FirstOrDefaultAsync(d => d.DistributorId == DistributorId && d.IsActive == true);

                if (Result != null)
                {
                    var Model = new DistributorViewModel
                    {
                        DistributorName = Result.DistributorName,
                        DistributorType = Result.DistributorType,
                        DistributorEmail = Result.DistributorEmail,
                        DistributorPhone = Result.DistributorPhone,
                        DistributorAddress = Result.DistributorAddress
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

        public async Task<List<DistributorViewModel>> GetDistributors()
        {
            try
            {
                DistributorViewModel Model = new DistributorViewModel();
                IEnumerable<DistributorViewModel> Distributer = _context.Distributors
                      .Where(d => d.IsActive == true)
                      .Select(d => new DistributorViewModel
                      {
                          DistributorId = d.DistributorId,
                          DistributorName = d.DistributorName,
                          DistributorType = d.DistributorType,
                          DistributorEmail = d.DistributorEmail,
                          DistributorPhone = d.DistributorPhone,
                          DistributorAddress = d.DistributorAddress
                      });

                return Distributer.ToList();
            }
            catch (Exception)
            {

                return null;
            }
        }

        public async Task<bool> UpdateDistributor(DistributorViewModel Model)
        {
            try
            {
                var Result = await _context.Distributors.FirstOrDefaultAsync(d => d.DistributorId == Model.DistributorId);
                if (Result != null)
                {
                    Result.DistributorName = Model.DistributorName;
                    Result.DistributorType = Model.DistributorType;
                    Result.DistributorEmail = Model.DistributorEmail;
                    Result.DistributorPhone = Model.DistributorPhone;
                    Result.DistributorAddress = Model.DistributorAddress;

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
