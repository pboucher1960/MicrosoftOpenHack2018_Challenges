using System.Runtime.Serialization;

namespace OpenHack.Challenge02.Models
{
    [DataContract]
    public class Product
    {
        [DataMember(Name = "productId")]
        public string ProductId { get; set; }
        [DataMember(Name = "roductName")]
        public string ProductName { get; set; }
        [DataMember(Name = "productDescription")]
        public string ProductDescription { get; set; }
    }
}