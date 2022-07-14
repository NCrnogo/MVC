using System;
using System.Collections.Generic;

namespace TeamyAPI.ModelsEF
{
    public partial class ProjectUserMapping
    {
        public int IdprojectUserMapping { get; set; }
        public int? UserFk { get; set; }
        public int? ProjectFk { get; set; }

        public virtual Project ProjectFkNavigation { get; set; }
        public virtual User UserFkNavigation { get; set; }
    }
}
