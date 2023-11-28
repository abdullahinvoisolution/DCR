using System;
using System.Collections.Generic;

namespace DAL.EntityModels
{
    public partial class RoleMenu
    {
        public int RoleMenuId { get; set; }
        public int? RoleId { get; set; }
        public int? MenuId { get; set; }
    }
}
