using DAL.EntityModels;
using DCR.Helper.ViewModel;
using DCR.ViewModel.IRepos;
using DCR.ViewModel.ViewModel;
using Microsoft.EntityFrameworkCore;


namespace Repository.Repos
{
    public class ContactPersonRepos : IContactPersonRepos
    {

        private readonly EMS_ITCContext _context;


        public ContactPersonRepos(EMS_ITCContext context)
        {
            _context = context;
        }

        public async Task<bool> AddPerson(ContactPersonViewModel model)
        {
            try
            {
                // Create a new User entity
                var NewPerson = new ContactPerson
                {
                    ContactPersonFirstName = model.ContactPersonFirstName,
                    ContactPersonLastName = model.ContactPersonLastName,
                    ContactPersonTitle = model.ContactPersonTitle,
                    ContactPersonPhoneNumber=model.ContactPersonPhoneNumber,
                    ContactPersonEmail=model.ContactPersonEmail,
                    CreatedBy = "Admin"
                };

                _context.ContactPeople.Add(NewPerson);
                await _context.SaveChangesAsync();

                var personid = new Vendor
                {
                    ContactPersonId = NewPerson.ContactPersonId,
                };

                _context.Vendors.Add(personid);
                await _context.SaveChangesAsync();


                var persondistributorid = new Distributor
                {
                    ContactPersonId = NewPerson.ContactPersonId,
                };

                _context.Distributors.Add(persondistributorid);
                await _context.SaveChangesAsync();

                var contactPersonViewModel = new ContactPersonViewModel
                {
                    ContactPersonFirstName = NewPerson.ContactPersonFirstName,
                    ContactPersonLastName = NewPerson.ContactPersonLastName,
                    ContactPersonTitle = NewPerson.ContactPersonTitle,
                    ContactPersonPhoneNumber = NewPerson.ContactPersonPhoneNumber,
                    ContactPersonEmail = NewPerson.ContactPersonEmail,
                };

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<bool> DeletePerson(int ContactPersonId)
        {
            var result = await _context.ContactPeople.Where(a => a.ContactPersonId == ContactPersonId).FirstOrDefaultAsync();
            if (result != null)
            {
                result.IsActive = false; // Mark the customer as inactive
                await _context.SaveChangesAsync();
                return true;
            }
            return false;
        }

        public async Task<ContactPersonViewModel> GetPerson(int ContactPersonId)
        {
            var result = await _context.ContactPeople.FirstOrDefaultAsync(a => a.ContactPersonId == ContactPersonId && a.IsActive == true);


            if (result != null)
            {
                var ContactPersonViewModel = new ContactPersonViewModel
                {
                    ContactPersonFirstName = result.ContactPersonFirstName,
                    ContactPersonLastName = result.ContactPersonLastName,
                    ContactPersonTitle = result.ContactPersonTitle,
                    ContactPersonPhoneNumber = result.ContactPersonPhoneNumber,
                    ContactPersonEmail = result.ContactPersonEmail,
                };

                return ContactPersonViewModel;
            }
            else
            {
                return null;
            }
        }

        public async Task<List<ContactPersonViewModel>> GetPersons(DataTableViewModel model)
        {
            IEnumerable<ContactPersonViewModel> contacts = _context.ContactPeople
                 .Where(x => x.IsActive == true)
                 .Select(x => new ContactPersonViewModel
                 {
                     ContactPersonFirstName = x.ContactPersonFirstName,
                     ContactPersonLastName = x.ContactPersonLastName,
                     ContactPersonTitle = x.ContactPersonTitle,
                     ContactPersonPhoneNumber = x.ContactPersonPhoneNumber,
                     ContactPersonEmail = x.ContactPersonEmail,
                 });

            if (contacts != null && contacts.Any())
            {
                string sort = string.Empty;

                if (sort == model.sortColum)
                {
                    if (sort.Contains(model.sortColumDir))
                    {
                        contacts = contacts.OrderBy(x => x.ContactPersonId);
                    }
                    else
                    {
                        contacts = contacts.OrderByDescending(x => x.ContactPersonId);

                    }
                }
                var _contact = contacts.Skip(model.skip).Take(model.length
                   ).ToList();
                return _contact;
            }
            else
            {
                // Handle the case where no active branches are found
                return new List<ContactPersonViewModel>(); // Returning an empty list
            }
          }

        public async Task<bool> UpdatePerson(ContactPersonViewModel model)
        {

            var result = await _context.ContactPeople.FirstOrDefaultAsync(a => a.ContactPersonId == model.ContactPersonId);
            if (result != null)
            {
                result.ContactPersonFirstName = model.ContactPersonFirstName;
                result.ContactPersonLastName = model.ContactPersonLastName;
                result.ContactPersonTitle = model.ContactPersonTitle;
                result.ContactPersonPhoneNumber = model.ContactPersonPhoneNumber;
                result.ContactPersonEmail = model.ContactPersonEmail;

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
