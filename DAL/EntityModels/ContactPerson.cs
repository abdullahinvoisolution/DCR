using System;
using System.Collections.Generic;

namespace DAL.EntityModels
{
    public partial class ContactPerson
    {
        public ContactPerson()
        {
            Distributors = new HashSet<Distributor>();
            Vendors = new HashSet<Vendor>();
        }

        public int ContactPersonId { get; set; }
        public string? ContactPersonFirstName { get; set; }
        public string? ContactPersonLastName { get; set; }
        public string? ContactPersonTitle { get; set; }
        public string? ContactPersonPhoneNumber { get; set; }
        public string? ContactPersonEmail { get; set; }
        public DateTime? CreatedOn { get; set; }
        public DateTime? UpdatedOn { get; set; }
        public string? CreatedBy { get; set; }
        public string? UpdatedBy { get; set; }
        public bool? IsActive { get; set; }

        public virtual ICollection<Distributor> Distributors { get; set; }
        public virtual ICollection<Vendor> Vendors { get; set; }
    }
}
