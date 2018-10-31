using System;

namespace IceCreamRatingsApi.Models
{
    public class Rating:IRating
    {
        public string Id { get; set; }
        public string UserId { get; set; }
        public string ProductId { get; set; }
        public string LocationName { get; set; }
        public int? Value { get; set; }
        public string UserNotes { get; set; }
        public DateTime TimeStamp { get ; set; }
        public string RatingValue { get; set; }

        public Rating()
        {
            Id = Guid.NewGuid().ToString();
            TimeStamp = DateTime.UtcNow;
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
