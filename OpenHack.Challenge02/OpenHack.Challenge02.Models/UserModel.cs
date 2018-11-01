using System.Runtime.Serialization;

namespace IceCreamRatingsApi.Models
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