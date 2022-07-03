using System.Runtime.Serialization;

namespace TeamyAPI.Models
{
    [DataContract]
    public class InviteUser
    {
        [DataMember(Order = 0)]
        public string UserId { get; set; }
        [DataMember(Order = 1)]
        public string TeamName { get; set; }
    }
}
