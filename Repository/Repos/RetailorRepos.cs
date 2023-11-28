using DAL.EntityModels;
using DCR.Helper.ViewModel;
using DCR.ViewModel.IRepos;
using DCR.ViewModel.ViewModel;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics.Metrics;
using System.Net;
using Twilio.Types;


namespace Repository.Repos
{
    public class RetailorRepos : IRetailorRepos
    {
        private readonly EMS_ITCContext _context;

        public RetailorRepos(EMS_ITCContext context)
        {
            _context = context;
        }

        public async Task<bool> AddRetailer(RetailerViewModel Model)
        {
            try
            {
                var NewRetailer = new Retailer
                {
                    RetailerName = Model.RetailerName,
                    Address = Model.Address,
                    PhoneNumber = Model.PhoneNumber,
                    Country = Model.Country,
                    City = Model.City,
                    State = Model.State,
                    SalesRegion = Model.SalesRegion,
                    PostalCode = Model.PostalCode,
                    IsActive = true

                };

                _context.Retailers.Add(NewRetailer);
                await _context.SaveChangesAsync();

                return true;
            }
            catch (Exception)
            {

                return false;
            }
        }

        public async Task<bool> DeleteRetailer(int RetailerId)
        {
            try
            {
                var Result = await _context.Retailers.Where(rt => rt.RetailerId == RetailerId).FirstOrDefaultAsync();
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

        public async Task<RetailerViewModel> GetRetailer(int RetailerId)
        {
            try
            {
                var Result = await _context.Retailers.FirstOrDefaultAsync(rt => rt.RetailerId == RetailerId && rt.IsActive == true);

                if (Result != null)
                {
                    var Model = new RetailerViewModel
                    {
                        RetailerName = Result.RetailerName,
                        Address = Result.Address,
                        PhoneNumber = Result.PhoneNumber,
                        Country = Result.Country,
                        City = Result.City,
                        State = Result.State,
                        SalesRegion = Result.SalesRegion,
                        PostalCode = Result.PostalCode,
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

        public async Task<List<RetailerViewModel>> GetRetailers()
        {
            try
            {
                RetailerViewModel Model = new RetailerViewModel();
                IEnumerable<RetailerViewModel> RetailerLists = _context.Retailers
                      .Where(rt => rt.IsActive == true)
                      .Select(rt => new RetailerViewModel
                      {
                          RetailerId = rt.RetailerId,
                          RetailerName = rt.RetailerName,
                          Address = rt.Address,
                          PhoneNumber = rt.PhoneNumber,
                          Country = rt.Country,
                          City = rt.City,
                          State = rt.State,
                          SalesRegion = rt.SalesRegion,
                          PostalCode = rt.PostalCode,
                      });

                return RetailerLists.ToList();
            }
            catch (Exception)
            {
                return null;
            }
        }

        public async Task<bool> UpdateRetailer(RetailerViewModel Model)
        {
            var Result = await _context.Retailers.FirstOrDefaultAsync(a => a.RetailerId == Model.RetailerId);
            if (Result != null)
            {
                Result.RetailerName = Model.RetailerName;
                Result.Address = Model.Address;
                Result.PhoneNumber = Model.PhoneNumber;
                Result.Country = Model.Country;
                Result.City = Model.City;
                Result.State = Model.State;
                Result.SalesRegion = Model.SalesRegion;
                Result.PostalCode = Model.PostalCode;

                Result.UpdatedOn = DateTime.Now;
                await _context.SaveChangesAsync();

                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
