using System.Runtime.Serialization;

namespace OpenHack.Challenge02.Models
{
    [DataContract]
    public class User
    {
        [DataMember(Name = "uerId")]
        public string UserId { get; set; }
        [DataMember(Name="userName")]
        public string UserName { get; set; }
        [DataMember(Name = "fullName")]
        public string FullName { get; set; }
    }
}