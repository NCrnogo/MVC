using System.Runtime.Serialization;

namespace TeamyAPI.Models
{
    [DataContract]
    public class Teams
    {
        [DataMember(Order = 0)]
        public int Id { get; set; }
        [DataMember(Order = 1)]
        public string Name { get; set; }
        [DataMember(Order = 2)]
        public int TeacherID { get; set; }
        [DataMember(Order = 3)]
        public int? OwnerID { get; set; }
        [DataMember(Order = 4)]
        public string DateCreated { get; set; }
        [DataMember(Order = 5)]
        public string OwnerName { get; set; }
        [DataMember(Order = 6)]
        public string TeacherName { get; set; }
    }
}
