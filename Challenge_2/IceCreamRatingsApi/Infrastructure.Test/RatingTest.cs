using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace IceCreamRatingsApi.Infrastructure.Test
{
    [TestClass]
    public class RatingTest
    {
        [TestMethod]
        public void FindReturnsCollection()
        {

            var rating = new Infrastructure.RatingRepository();

            var result = rating.FindAsync();

        }
    }
}
