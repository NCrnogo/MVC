using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Runtime.Serialization;

namespace TeamyAPI.Models
{
    [DataContract]
    public class Users
    {
        [DataMember(Order = 0)]
        public int Id { get; set; }
        [DataMember(Order = 1)]
        public string Name { get; set; }
        [DataMember(Order = 2)]
        public string Roll { get; set; }
        [DataMember(Order = 3)]
        public string Pwd { get; set; }
        [DataMember(Order = 4)]
        public string DateCreated { get; set; }
    }
}
