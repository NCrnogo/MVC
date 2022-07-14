using System;
using System.Collections.Generic;

namespace TeamyAPI.ModelsEF
{
    public partial class TeamMember
    {
        public int IdteamMembers { get; set; }
        public int TeamId { get; set; }
        public int UserId { get; set; }

        public virtual Team Team { get; set; }
        public virtual User User { get; set; }
    }
}
