using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Runtime.Serialization;

namespace Teamy.Models
{
    [DataContract (Name = "http://localhost:5000/api/User")]
    public class Users
    {
        [DataMember(Order = 0)]
        public int Id { get; set; }
        [Required]
        [DataType(DataType.EmailAddress)]
        [DataMember(Order = 1)]
        public string Name { get; set; }
        [Required]
        [DataMember(Order = 2)]
        public string Roll { get; set; }
        [Required]
        [DataType(DataType.Password)]
        [DataMember(Order = 3)]
        public string Pwd { get; set; }
        [DataMember(Order = 4)]
        public string DateCreated { get; set; }
    }
}