using System;
using System.Collections.Generic;

namespace Infrastructure
{
    public class Rating
    {
        public IEnumerable<IRating> Find()
        {
            throw new NotImplementedException();
        }

        public IRating Find(string ratingId)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<IRating> FindByUser(string userId)
        {
            throw new NotImplementedException();
        }
    }
}
