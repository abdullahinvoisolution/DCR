using DCR.Helper.ViewModel;
using DCR.ViewModel.ViewModel;

namespace DCR.ViewModel.IRepos
{ 
    public interface IContactPersonRepos
    {
        Task<List<ContactPersonViewModel>> GetPersons(DataTableViewModel model);
        Task<ContactPersonViewModel> GetPerson(int ContactPersonId);
        Task<bool> AddPerson(ContactPersonViewModel model);
        Task<bool> UpdatePerson(ContactPersonViewModel model);
        Task<bool> DeletePerson(int ContactPersonId);
    }
}
