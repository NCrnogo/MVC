using System;
using System.Collections.Generic;

namespace TeamyAPI.ModelsEF
{
    public partial class UserRole
    {
        public UserRole()
        {
            UserRollMappings = new HashSet<UserRollMapping>();
        }

        public int IduserRole { get; set; }
        public string Roll { get; set; }

        public virtual ICollection<UserRollMapping> UserRollMappings { get; set; }
    }
}
