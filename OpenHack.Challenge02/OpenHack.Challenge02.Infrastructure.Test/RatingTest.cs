using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;

namespace IceCreamRatingsApi.Infrastructure.Test
{
    [TestClass]
    public class RatingTest
    {
        [TestMethod]
        public void FindReturnsCollection()
        {
            var rating = new UserRatingManager();
            var result = rating.Find();
            Assert.IsTrue(result.Any());
        }

        [TestMethod]
        public void AddCreatesDocument()
        {

            var rating = new UserRatingManager();

            var item = new Models.UserRating
            {
                Id = Guid.NewGuid().ToString(),
                LocationName = "Test",
                RatingValue = "5",
                TimeStamp = DateTime.Now
            };

           UserRatingManager.AddAsync(item).Wait();

        }
    }
}
