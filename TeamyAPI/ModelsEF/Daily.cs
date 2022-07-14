using System;
using System.Collections.Generic;

namespace TeamyAPI.ModelsEF
{
    public partial class Daily
    {
        public Daily()
        {
            Activities = new HashSet<Activity>();
        }

        public int Iddaily { get; set; }
        public string DateCreated { get; set; }
        public string Details { get; set; }
        public int? UserFk { get; set; }

        public virtual User UserFkNavigation { get; set; }
        public virtual ICollection<Activity> Activities { get; set; }
    }
}
