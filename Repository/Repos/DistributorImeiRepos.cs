using DAL.EntityModels;
using DCR.ViewModel.IRepos;
using DCR.ViewModel.ViewModel;
using Microsoft.EntityFrameworkCore;


namespace Repository.Repos
{
    public class DistributorImeiRepos : IDistributorIMEIRepos
    {
        private readonly EMS_ITCContext _context;

        public DistributorImeiRepos(EMS_ITCContext context)
        {
            _context = context;
        }

        public async Task<bool> AddDistributorImei(DistibutorIMEIViewModel Model)
        {
            try
            {
                var NewDistributor = new DistributorImei
                {
                    DistributorId = Model.DistributorId,
                    ImeiId = Model.ImeiId
                };

                _context.DistributorImeis.Add(NewDistributor);
                await _context.SaveChangesAsync();

                return true;
            }
            catch (Exception)
            {

                return false;
            }
        }

        public async Task<bool> DeleteDistributorImei(int DistributorImeiId)
        {
            try
            {
                var Result = await _context.DistributorImeis.Where(d => d.DistributorImeiId == DistributorImeiId).FirstOrDefaultAsync();
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

        public async Task<DistibutorIMEIViewModel> GetDistributorImei(int DistributorImeiId)
        {
            try
            {
                var Result = await _context.DistributorImeis.FirstOrDefaultAsync(d => d.DistributorImeiId == DistributorImeiId);

                if (Result != null)
                {
                    var Model = new DistibutorIMEIViewModel
                    {
                        DistributorId = Result.DistributorId,
                        ImeiId = Result.ImeiId,
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

        public async Task<List<DistibutorIMEIViewModel>> GetDistributorImeis()
        {
            try
            {
                DistibutorIMEIViewModel Model = new DistibutorIMEIViewModel();
                IEnumerable<DistibutorIMEIViewModel> DistributorImeiLists = _context.DistributorImeis
                      .Select(d => new DistibutorIMEIViewModel
                      {
                          DistributorId = d.DistributorId,
                          ImeiId = d.ImeiId,
                      });

                return DistributorImeiLists.ToList();
            }
            catch (Exception)
            {

                return null;
            }
        }

        public async Task<bool> UpdateDistributorImei(DistibutorIMEIViewModel Model)
        {
            try
            {
                var Result = await _context.DistributorImeis.FirstOrDefaultAsync(d => d.DistributorImeiId == Model.DistributorImeiId);
                if (Result != null)
                {

                    Result.DistributorId = Model.DistributorId;
                    Result.ImeiId = Model.ImeiId;

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
