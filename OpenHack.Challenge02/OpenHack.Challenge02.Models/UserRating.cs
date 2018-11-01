namespace OpenHack.Challenge02.Models
{
    using Newtonsoft.Json;
    using System;
    using System.Runtime.Serialization;

    public class UserRating:IUserRating
    {
        [JsonProperty(PropertyName = "id")]
        public string Id { get; set; }

        [JsonProperty(PropertyName = "userId")]
        public string UserId { get; set; }

        [JsonProperty(PropertyName = "productId")]
        public string ProductId { get; set; }

        [JsonProperty(PropertyName = "locationName")]
        public string LocationName { get; set; }

        [JsonProperty(PropertyName = "rating")]
        public int? Rating { get; set; }

        [DataMember(Name = "userNotes")]
        public string UserNotes { get; set; }

        [JsonProperty(PropertyName = "timeStamp")]
        public DateTime TimeStamp { get ; set; }


        public string RatingValue { get; set; }

        public UserRating()
        {
            TimeStamp = DateTime.UtcNow;
            Id = Guid.NewGuid().ToString();
        }

        public bool IsValid()
        {
            if (string.IsNullOrEmpty(UserId))
            {
                return false;
            }

            if (string.IsNullOrEmpty(ProductId))
            {
                return false;
            }

            return true;
        }
    }
}
