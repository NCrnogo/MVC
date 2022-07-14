using System;
using System.Collections.Generic;

namespace TeamyAPI.ModelsEF
{
    public partial class TeamInvite
    {
        public int IdteamInvites { get; set; }
        public int TeamId { get; set; }
        public int UserId { get; set; }
        public int? Invited { get; set; }

        public virtual Team Team { get; set; }
        public virtual User User { get; set; }
    }
}
