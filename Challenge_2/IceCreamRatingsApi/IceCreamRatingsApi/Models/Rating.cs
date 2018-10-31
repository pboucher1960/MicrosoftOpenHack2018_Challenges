using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IceCreamRatingsApi.Models
{
    public class Rating
    {
        public string UserId { get; set; }
        public string ProductId { get; set; }
        public string LocationName { get; set; }
        public int? Value { get; set; }
        public string UserNotes { get; set; }
        public DateTime Timestamp { get; set; }

        public Rating()
        {
            Timestamp = DateTime.UtcNow;
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
