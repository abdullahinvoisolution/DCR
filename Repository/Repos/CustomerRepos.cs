using DAL.EntityModels;
using DCR.Helper.ViewModel;
using DCR.ViewModel.IRepos;
using Microsoft.EntityFrameworkCore;

namespace Repository.Repos
{
    public class CustomerRepos : ICustomerRepos
    {
        private readonly EMS_ITCContext _context;


        public CustomerRepos(EMS_ITCContext context)
        {
            _context = context;
        }

        public async Task<bool> AddCustomer(CustomerViewModel Model)
        {
            try
            {
                var NewCustomer = new Customer
                {
                    CustomerName = Model.CustomerName,
                    CustomerType = Model.CustomerType,
                    CustomerEmail = Model.CustomerEmail,
                    CustomerPhone = Model.CustomerPhone,
                    CustomerCountry = Model.CustomerCountry,
                    CustomerAddress = Model.CustomerAddress,
                    CustomerCity = Model.CustomerCity,
                    CustomerState = Model.CustomerState,
                    CustomerPostalCode = Model.CustomerPostalCode
                };
                _context.Customers.Add(NewCustomer);
                await _context.SaveChangesAsync();

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<bool> DeleteCustomer(int CustomerId)
        {

            try
            {
                var Result = await _context.Customers.Where(a => a.CustomerId == CustomerId).FirstOrDefaultAsync();
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
        public async Task<List<CustomerViewModel>> GetCustomers()
        {
            try
            {
                CustomerViewModel Model = new CustomerViewModel();
                IEnumerable<CustomerViewModel> Customer = _context.Customers
                      .Where(c => c.IsActive == true)
                      .Select(c => new CustomerViewModel
                      {
                          CustomerId = c.CustomerId,
                          CustomerName = c.CustomerName,
                          CustomerType = c.CustomerType,
                          CustomerEmail = c.CustomerEmail,
                          CustomerPhone = c.CustomerPhone,
                          CustomerCountry = c.CustomerCountry,
                          CustomerAddress = c.CustomerAddress,
                          CustomerCity = c.CustomerCity,
                          CustomerState = c.CustomerState,
                          CustomerPostalCode = c.CustomerPostalCode
                      });

                return Customer.ToList();
            }
            catch (Exception)
            {

                return null;
            }
        }

        public async Task<CustomerViewModel> GetCustomer(int CustomerId)
        {
            try
            {
                var Result = await _context.Customers.FirstOrDefaultAsync(c => c.CustomerId == CustomerId && c.IsActive == true);

                if (Result != null)
                {
                    var Model = new CustomerViewModel
                    {
                        CustomerName = Result.CustomerName,
                        CustomerType = Result.CustomerType,
                        CustomerEmail = Result.CustomerEmail,
                        CustomerPhone = Result.CustomerPhone,
                        CustomerCountry = Result.CustomerCountry,
                        CustomerAddress = Result.CustomerAddress,
                        CustomerCity = Result.CustomerCity,
                        CustomerState = Result.CustomerState,
                        CustomerPostalCode = Result.CustomerPostalCode
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

        public async Task<bool> UpdateCustomer(CustomerViewModel Model)
        {
            var Result = await _context.Customers.FirstOrDefaultAsync(a => a.CustomerId == Model.CustomerId);
            if (Result != null)
            {

                Result.CustomerName = Model.CustomerName;
                Result.CustomerType = Model.CustomerType;
                Result.CustomerEmail = Model.CustomerEmail;
                Result.CustomerPhone = Model.CustomerPhone;
                Result.CustomerCountry = Model.CustomerCountry;
                Result.CustomerAddress = Model.CustomerAddress;
                Result.CustomerCity = Model.CustomerCity;
                Result.CustomerState = Model.CustomerState;
                Result.CustomerPostalCode = Model.CustomerPostalCode;

                Result.UpdatedBy = Model.CustomerName; 
                Result.UpdatedOn = DateTime.Now; 
                                                 
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
