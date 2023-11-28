

namespace DAL.EntityModels
{
    public partial class MenuList
    {
        public MenuList()
        {
            MenuPermissionAssigns = new HashSet<MenuPermissionAssign>();
        }

        public int MenuListId { get; set; }
        public string? MenuListTitle { get; set; }
        public string? MenuListDescription { get; set; }
        public int? MenuListParentId { get; set; }
        public bool? MenuListHasChildren { get; set; }
        public int? MenuListSortOrder { get; set; }
        public string? MenuListNavigationUrl { get; set; }
        public string? MenuListIconClass { get; set; }
        public bool? IsActive { get; set; }
        public DateTime? CreatedOn { get; set; }
        public DateTime? UpdatedOn { get; set; }
        public string? CreatedBy { get; set; }
        public string? UpdatedBy { get; set; }

        public virtual ICollection<MenuPermissionAssign> MenuPermissionAssigns { get; set; }
    }
}
