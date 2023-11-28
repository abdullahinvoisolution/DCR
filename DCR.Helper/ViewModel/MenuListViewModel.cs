
namespace DCR.ViewModel.ViewModel
{
    public class MenuListViewModel
    {
        public int MenuListId { get; set; }
        public string? MenuListTitle { get; set; }
        public string? MenuListDescription { get; set; }
        public int? MenuListParentId { get; set; }
        public bool? MenuListHasChildren { get; set; }
        public int? MenuListSortOrder { get; set; }
        public string? MenuListNavigationUrl { get; set; }
        public string? MenuListIconClass { get; set; }
    }
}
