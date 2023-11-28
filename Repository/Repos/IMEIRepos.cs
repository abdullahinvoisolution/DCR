using DAL.EntityModels;
using DCR.Helper.ViewModel;
using DCR.ViewModel.IRepos;
using DCR.ViewModel.ViewModel;
using Microsoft.EntityFrameworkCore;



namespace Repository.Repos
{
    public class IMEIRepos : IIMEIRepos
    {

        private readonly EMS_ITCContext _context;

        public IMEIRepos(EMS_ITCContext context)
        {
            _context = context;
        }

        public async Task<bool> AddIMEI(IMEIViewModel Model)
        {
            try
            {
                var NewIMEI = new Imei
                {
                    ProductId = Model.ProductId,
                    ImeiNumber = Model.ImeiNumber,
                    ImeiNumber2 = Model.ImeiNumber2,
                    ImeiStatus = Model.ImeiStatus,
                    DeviceType = Model.DeviceType,
                    ActivationDate = Model.ActivationDate,
                    IsActive = true,
                };

                _context.Imeis.Add(NewIMEI);
                await _context.SaveChangesAsync();

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<bool> DeleteIMEI(int ImeiId)
        {
            try
            {
                var Result = await _context.Imeis.Where(a => a.ImeiId == ImeiId).FirstOrDefaultAsync();
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

        public async Task<IMEIViewModel> GetIMEI(int ImeiId)
        {
            try
            {
                var Result = await _context.Imeis.FirstOrDefaultAsync(i => i.ImeiId == ImeiId && i.IsActive == true);

                if (Result != null)
                {
                    var Model = new IMEIViewModel
                    {
                        ProductId = Result.ProductId,
                        ImeiNumber = Result.ImeiNumber,
                        ImeiNumber2 = Result.ImeiNumber2,
                        ImeiStatus = Result .ImeiStatus,
                        DeviceType = Result.DeviceType,
                        ActivationDate = Result.ActivationDate,
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

        public async Task<List<IMEIViewModel>> GetIMEIs()
        {
            try
            {
                IMEIViewModel Model = new IMEIViewModel();
                IEnumerable<IMEIViewModel> ImeiLists = _context.Imeis
                      .Where(i => i.IsActive == true)
                      .Select(i => new IMEIViewModel
                      {
                          ImeiId = Model.ImeiId,
                          ProductId = Model.ProductId,
                          ImeiNumber = Model.ImeiNumber,
                          ImeiNumber2 = Model.ImeiNumber2,
                          ImeiStatus = Model.ImeiStatus,
                          DeviceType = Model.DeviceType,
                          ActivationDate = Model.ActivationDate,
                      });

                return ImeiLists.ToList();
            }
            catch (Exception)
            {

                return null;
            }
        }

        public async Task<bool> UpdateIMEI(IMEIViewModel Model)
        {
            try
            {
                var Result = await _context.Imeis.FirstOrDefaultAsync(i => i.ImeiId == Model.ImeiId);
                if (Result != null)
                {
                    Result.ProductId = Model.ProductId;
                    Result.ImeiNumber = Model.ImeiNumber;
                    Result.ImeiNumber2 = Model.ImeiNumber2;
                    Result.ImeiStatus = Model.ImeiStatus;
                    Result.DeviceType = Model.DeviceType;
                    Result.ActivationDate = Model.ActivationDate;

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
