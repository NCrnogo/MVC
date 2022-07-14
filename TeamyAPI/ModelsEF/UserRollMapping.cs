using System;
using System.Collections.Generic;

namespace TeamyAPI.ModelsEF
{
    public partial class UserRollMapping
    {
        public int IduserRoleMapping { get; set; }
        public int UserFk { get; set; }
        public int UserRoleFk { get; set; }

        public virtual User UserFkNavigation { get; set; }
        public virtual UserRole UserRoleFkNavigation { get; set; }
    }
}
