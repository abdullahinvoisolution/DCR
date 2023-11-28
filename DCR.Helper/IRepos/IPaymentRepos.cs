using DCR.ViewModel.ViewModel;


namespace DCR.ViewModel.IRepos
{
    public interface IPaymentRepos
    {
        Task<List<PaymentViewModel>> GetPayments();
        Task<PaymentViewModel> GetPayment(int Paymentid);
        Task<bool> AddPayment(PaymentViewModel Model);
        Task<bool> UpdatePayment(PaymentViewModel Model);
        Task<bool> DeletePayment(int Paymentid);
    }
}
