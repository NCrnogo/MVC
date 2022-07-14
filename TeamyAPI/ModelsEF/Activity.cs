using System;
using System.Collections.Generic;

namespace TeamyAPI.ModelsEF
{
    public partial class Activity
    {
        public int Idactivities { get; set; }
        public string Start { get; set; }
        public string End { get; set; }
        public int? DailyFk { get; set; }

        public virtual Daily DailyFkNavigation { get; set; }
    }
}
