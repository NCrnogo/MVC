using System;
using System.Collections.Generic;

namespace TeamyAPI.ModelsEF
{
    public partial class Team
    {
        public Team()
        {
            TeamInvites = new HashSet<TeamInvite>();
            TeamMembers = new HashSet<TeamMember>();
        }

        public int Idteam { get; set; }
        public string Team1 { get; set; }
        public string Created { get; set; }
        public int OwnerId { get; set; }
        public int? TeacherId { get; set; }

        public virtual User Owner { get; set; }
        public virtual User Teacher { get; set; }
        public virtual ICollection<TeamInvite> TeamInvites { get; set; }
        public virtual ICollection<TeamMember> TeamMembers { get; set; }
    }
}
