using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace Teamy.Models
{
    [DataContract(Name = "http://localhost:5000/api/User")]
    public class InviteUser
    {
        [DataMember(Order = 0)]
        public string UserId { get; set; }
        [DataMember(Order = 1)]
        public string TeamName { get; set; }
    }
}