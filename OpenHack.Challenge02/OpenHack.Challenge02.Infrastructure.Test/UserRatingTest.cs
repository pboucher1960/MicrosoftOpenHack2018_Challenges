using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;

namespace OpenHack.Challenge02.Infrastructure.Test
{
    [TestClass]
    public class UserRatingTest
    {
        [TestMethod]
        public void FindReturnsCollection()
        {
            var rating = new UserRatingManager();
            var result = rating.Find();
            Assert.IsTrue(result.Any());
        }

        [TestMethod]
        public void FindByUserIdReturnsCollection()
        {
            var rating = new UserRatingManager();
            var result = rating.FindByUserId("Test-UserId");
            Assert.IsTrue(result.Any());
        }

        [TestMethod]
        public void AddCreatesDocument()
        {

            var rating = new UserRatingManager();

            var item = new Models.UserRating
            {
                Id = Guid.NewGuid().ToString(),
                UserId = "Test-UserId",
                LocationName = "Test-Location",
                RatingValue = "5",
                UserNotes = "Test-UserNotes",
                TimeStamp = DateTime.Now
            };

           UserRatingManager.AddAsync(item).Wait();

        }
    }
}
