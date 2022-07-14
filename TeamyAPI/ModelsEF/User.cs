using System;
using System.Collections.Generic;

namespace TeamyAPI.ModelsEF
{
    public partial class User
    {
        public User()
        {
            Dailies = new HashSet<Daily>();
            ProjectUserMappings = new HashSet<ProjectUserMapping>();
            Projects = new HashSet<Project>();
            TeamInvites = new HashSet<TeamInvite>();
            TeamMembers = new HashSet<TeamMember>();
            TeamOwners = new HashSet<Team>();
            TeamTeachers = new HashSet<Team>();
            UserRollMappings = new HashSet<UserRollMapping>();
        }

        public int Iduser { get; set; }
        public byte[] PasswordHash { get; set; }
        public string Salt { get; set; }
        public string LoginName { get; set; }
        public string DateCreated { get; set; }

        public virtual ICollection<Daily> Dailies { get; set; }
        public virtual ICollection<ProjectUserMapping> ProjectUserMappings { get; set; }
        public virtual ICollection<Project> Projects { get; set; }
        public virtual ICollection<TeamInvite> TeamInvites { get; set; }
        public virtual ICollection<TeamMember> TeamMembers { get; set; }
        public virtual ICollection<Team> TeamOwners { get; set; }
        public virtual ICollection<Team> TeamTeachers { get; set; }
        public virtual ICollection<UserRollMapping> UserRollMappings { get; set; }
    }
}
