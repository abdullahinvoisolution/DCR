using DCR.Helper.ViewModel;


namespace DCR.ViewModel.IRepos
{
    public interface ICustomerRepos
    {
        Task<List<CustomerViewModel>> GetCustomers();
        Task<CustomerViewModel> GetCustomer(int CustomerId);
        Task<bool> AddCustomer(CustomerViewModel Model);
        Task<bool> UpdateCustomer(CustomerViewModel Model);
        Task<bool> DeleteCustomer(int CustomerId);

    }
}
