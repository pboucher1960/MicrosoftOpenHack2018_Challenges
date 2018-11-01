using System;

namespace IceCreamRatingsApi.Models
{
    public interface IUserRating
    {
        string Id { get; set; }
        string ProductId { get; set; }

        DateTime TimeStamp { get; set; }

        string LocationName { get; set; }

        string RatingValue { get; set; }

        string UserNotes { get; set; }
    }
}
