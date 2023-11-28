using DAL.EntityModels;
using DCR.ViewModel.IRepos;
using DCR.ViewModel.ViewModel;
using Microsoft.EntityFrameworkCore;

namespace Repository.Repos
{
    public class ReceiveRepos : IReceiveRepos
    {
        private readonly EMS_ITCContext _context;

        public ReceiveRepos(EMS_ITCContext context)
        {
            _context = context;
        }

        public async Task<bool> AddReceive(ReceiveViewModel Model)
        {
            try
            {
                var NewReceive = new Receive
                {
                    DistributorId = Model.DistributorId,
                    ReceiveDate = Model.ReceiveDate,
                    ReceiptNumber = Model.ReceiptNumber,
                    Status = Model.Status,
                    Quantity = Model.Quantity,
                    IsActive = true
                };

                _context.Receives.Add(NewReceive);
                await _context.SaveChangesAsync();

                return true;
            }
            catch (Exception)
            {

                return false;
            }
        }

        public async Task<bool> DeleteReceive(int ReceiveId)
        {
            try
            {
                var Result = await _context.Receives.Where(r => r.ReceiveId == ReceiveId).FirstOrDefaultAsync();
                if (Result != null)
                {
                    Result.IsActive = false; // Mark the customer as inactive
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

        public async Task<ReceiveViewModel> GetReceive(int ReceiveId)
        {
            try
            {
                var Result = await _context.Receives.FirstOrDefaultAsync(r => r.ReceiveId == ReceiveId && r.IsActive == true);

                if (Result != null)
                {
                    var Model = new ReceiveViewModel
                    {
                        DistributorId = Result.DistributorId,
                        ReceiveDate = Result.ReceiveDate,
                        ReceiptNumber = Result.ReceiptNumber,
                        Status = Result.Status,
                        Quantity = Result.Quantity,
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

        public async Task<List<ReceiveViewModel>> GetReceives()
        {
            try
            {
                ReceiveViewModel Model = new ReceiveViewModel();
                IEnumerable<ReceiveViewModel> RecieveLists = _context.Receives
                      .Where(r => r.IsActive == true)
                      .Select(r => new ReceiveViewModel
                      {
                          ReceiveId = r.ReceiveId,
                          DistributorId = r.DistributorId,
                          ReceiveDate = r.ReceiveDate,
                          ReceiptNumber = r.ReceiptNumber,
                          Status = r.Status,
                          Quantity = r.Quantity,
                      });

                return RecieveLists.ToList();
            }
            catch (Exception)
            {
                return null;
            }
        }

        public async Task<bool> UpdateReceive(ReceiveViewModel Model)
        {
            var Result = await _context.Receives.FirstOrDefaultAsync(a => a.ReceiveId == Model.ReceiveId);
            if (Result != null)
            {

                Result.ReceiveDate = Model.ReceiveDate;
                Result.ReceiptNumber = Model.ReceiptNumber;
                Result.Status = Model.Status;
                Result.Quantity = Model.Quantity;

                Result.UpdatedOn = DateTime.Now;  
                await _context.SaveChangesAsync();

                return true;

            }
            else
            {
                //  not found
                return false;
            }
        }
    }
}
