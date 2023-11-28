using DAL.EntityModels;
using DCR.ViewModel.IRepos;
using DCR.ViewModel.ViewModel;
using Microsoft.EntityFrameworkCore;


namespace Repository.Repos
{
    public class PaymentRepos : IPaymentRepos
    {
        private readonly EMS_ITCContext _context;

        public PaymentRepos(EMS_ITCContext context)
        {
            _context = context;
        }
        public async Task<bool> AddPayment(PaymentViewModel Model)
        {
            try
            {
                var NewPayment = new Payment
                {
                    DistributorId = Model.DistributorId,
                    PaymentAmount = Model.PaymentAmount,
                    PaymentDate = Model.PaymentDate,
                    PaymentDescription = Model.PaymentDescription,
                    IsActive = true
                };
                _context.Payments.Add(NewPayment);
                await _context.SaveChangesAsync();

                return true;
            }
            catch (Exception)
            {

                return false;
            }
        }

        public async Task<bool> DeletePayment(int Paymentid)
        {
            try
            {
                var Result = await _context.Payments.Where(p => p.PaymentId == Paymentid).FirstOrDefaultAsync();
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

        public async Task<PaymentViewModel> GetPayment(int Paymentid)
        {
            try
            {
                var Result = await _context.Payments.FirstOrDefaultAsync(p => p.PaymentId == Paymentid && p.IsActive == true);

                if (Result != null)
                {
                    var Model = new PaymentViewModel
                    {
                        DistributorId = Result.DistributorId,
                        PaymentAmount = Result.PaymentAmount,
                        PaymentDate = Result.PaymentDate,
                        PaymentDescription = Result.PaymentDescription
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

        public async Task<List<PaymentViewModel>> GetPayments()
        {
            try
            {
                PaymentViewModel Model = new PaymentViewModel();
                IEnumerable<PaymentViewModel> PaymentLists = _context.Payments
                      .Where(p => p.IsActive == true)
                      .Select(p => new PaymentViewModel
                      {
                          PaymentId = p.PaymentId,
                          DistributorId = p.DistributorId,
                          PaymentAmount = p.PaymentAmount,
                          PaymentDate = p.PaymentDate,
                          PaymentDescription = p.PaymentDescription
                      });

                return PaymentLists.ToList();
            }
            catch (Exception)
            {
                return null;
            }
        }

        public async Task<bool> UpdatePayment(PaymentViewModel Model)
        {
            try
            {
                var Result = await _context.Payments.Where(p => p.PaymentId == Model.PaymentId).FirstOrDefaultAsync();
                if (Result != null)
                {
                    Result.DistributorId = Model.DistributorId;
                    Result.PaymentAmount = Model.PaymentAmount;
                    Result.PaymentDate = Model.PaymentDate;
                    Result.PaymentDescription = Model.PaymentDescription;

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
