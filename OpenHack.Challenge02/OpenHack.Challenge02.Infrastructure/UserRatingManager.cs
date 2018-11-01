namespace OpenHack.Challenge02.Infrastructure
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Net;
    using System.Threading.Tasks;
    using Microsoft.Azure.Documents;
    using Microsoft.Azure.Documents.Client;
    using OpenHack.Challenge02.Models;

    public class UserRatingManager 
    {
        private const string databaseName = "OpenHackTable08";
        private const string collectionName = "UserRatingCollection";
        private const string EndpointUrl = "https://openhacktable08.documents.azure.com:443";
        private const string PrimaryKey = "VZFwRgdsJhhlPXgeQVCmtgTBv1W09VICD91MYUXbIyVEEQRrVYvmOKasaZW6Pax1hsKfw7XRZeEa4Hp453LxjQ==";
        private static DocumentClient client = new DocumentClient(new Uri(EndpointUrl), PrimaryKey);


        public UserRatingManager()
        {
            Initialize().Wait();
        }


        private static async Task Initialize()
        {
            await client.CreateDocumentCollectionIfNotExistsAsync(UriFactory.CreateDatabaseUri(databaseName), new DocumentCollection { Id = "UserRatingCollection" });
        }

        public List<UserRating> Find()
        {
            try
            {
                FeedOptions queryOptions = new FeedOptions { MaxItemCount = -1 };
                IQueryable<UserRating> ratingQueryInSql = client.CreateDocumentQuery<UserRating>(
                UriFactory.CreateDocumentCollectionUri(databaseName, collectionName),
                "SELECT * FROM UserRatingCollection", queryOptions);

                System.Diagnostics.Debug.WriteLine("Running direct SQL query...");
                return ratingQueryInSql.ToList();

            }
            catch (DocumentClientException de)
            {
                Exception baseException = de.GetBaseException();
                Console.WriteLine("{0} error occurred: {1}, Message: {2}", de.StatusCode, de.Message, baseException.Message);
                throw;
            }
            catch (Exception e)
            {
                Exception baseException = e.GetBaseException();
                Console.WriteLine("Error: {0}, Message: {1}", e.Message, baseException.Message);
                throw;
            }

        }

        public IUserRating Find(string ratingId)
        {
            try
            {
                FeedOptions queryOptions = new FeedOptions { MaxItemCount = -1 };
                IQueryable<UserRating> ratingQueryInSql = client.CreateDocumentQuery<UserRating>(
                UriFactory.CreateDocumentCollectionUri(databaseName, collectionName),
                "SELECT * FROM UserRatingCollection WHERE UserRatingCollection.id = '" + ratingId + "'", queryOptions);
                System.Diagnostics.Debug.WriteLine("Running direct SQL query...");

                var result = ratingQueryInSql.ToList();

                return result.FirstOrDefault();

            }
            catch (DocumentClientException de)
            {
                Exception baseException = de.GetBaseException();
                Console.WriteLine("{0} error occurred: {1}, Message: {2}", de.StatusCode, de.Message, baseException.Message);
                throw;
            }
            catch (Exception e)
            {
                Exception baseException = e.GetBaseException();
                Console.WriteLine("Error: {0}, Message: {1}", e.Message, baseException.Message);
                throw;
            }
        }

        public IEnumerable<IUserRating> FindByUserId(string userId)
        {
            try
            {
                FeedOptions queryOptions = new FeedOptions { MaxItemCount = -1 };
                IQueryable<UserRating> ratingQueryInSql = client.CreateDocumentQuery<UserRating>(
                UriFactory.CreateDocumentCollectionUri(databaseName, collectionName),
                "SELECT * FROM UserRatingCollection WHERE UserRatingCollection.userId = '" + userId + "'", queryOptions);
                System.Diagnostics.Debug.WriteLine("Running direct SQL query...");
                return ratingQueryInSql.ToList();

            }
            catch (DocumentClientException de)
            {
                Exception baseException = de.GetBaseException();
                Console.WriteLine("{0} error occurred: {1}, Message: {2}", de.StatusCode, de.Message, baseException.Message);
                throw;
            }
            catch (Exception e)
            {
                Exception baseException = e.GetBaseException();
                Console.WriteLine("Error: {0}, Message: {1}", e.Message, baseException.Message);
                throw;
            }
        }

        public static async Task<UserRating> AddAsync(UserRating rating)
        {
            //THIS IS A CHANGE THAT DOES NOTHING
            System.Diagnostics.Debug.WriteLine("AddAsync");
            try
            {
                Document created = await client.CreateDocumentAsync(UriFactory.CreateDocumentCollectionUri(databaseName, collectionName), rating);
                System.Diagnostics.Debug.WriteLine(created.Id);

                UserRating result = new UserRating();
                result.Id = created.GetPropertyValue<string>("id");
                result.LocationName = created.GetPropertyValue<string>("locationName");
                result.Rating = created.GetPropertyValue<int>("rating");
                result.UserId = created.GetPropertyValue<string>("userId");
                result.ProductId = created.GetPropertyValue<string>("productId");
                result.TimeStamp =  created.GetPropertyValue<DateTime>("timeStamp");
                result.UserNotes = created.GetPropertyValue<string>("userNotes");
                result.Version = created.GetPropertyValue<string>("version");
                /* and so on, for all the properties of Employee */

                return result;
            }
            catch (DocumentClientException de)
            {
                System.Diagnostics.Debug.WriteLine(de.Message);
                throw;

            }
        }
    }
}
