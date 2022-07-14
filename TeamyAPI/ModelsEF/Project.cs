using System;
using System.Collections.Generic;

namespace TeamyAPI.ModelsEF
{
    public partial class Project
    {
        public Project()
        {
            ProjectUserMappings = new HashSet<ProjectUserMapping>();
        }

        public int Idproject { get; set; }
        public string Project1 { get; set; }
        public string Created { get; set; }
        public int? TeamLeadFk { get; set; }

        public virtual User TeamLeadFkNavigation { get; set; }
        public virtual ICollection<ProjectUserMapping> ProjectUserMappings { get; set; }
    }
}
