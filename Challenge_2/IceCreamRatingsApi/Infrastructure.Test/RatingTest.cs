using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Infrastructure.Test
{
    [TestClass]
    public class RatingTest
    {
        [TestMethod]
        public void FindReturnsCollection()
        {

            var rating = new Infrastructure.Rating();

            var result = rating.FindAsync();

        }
    }
}
