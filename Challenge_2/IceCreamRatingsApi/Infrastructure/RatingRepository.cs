using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.Azure.Documents;
using Microsoft.Azure.Documents.Client;
using IceCreamRatingsApi.Models.DbAccess;
using IceCreamRatingsApi.Models;

namespace IceCreamRatingsApi.Infrastructure
{
    public class RatingRepository : IRatingRepository
    {
        private const string databaseName = "OpenHackTable08";
        private const string collectionName = "RatingCollection";
        private const string EndpointUrl = "https://openhacktable08.documents.azure.com:443";
        private const string PrimaryKey = "VZFwRgdsJhhlPXgeQVCmtgTBv1W09VICD91MYUXbIyVEEQRrVYvmOKasaZW6Pax1hsKfw7XRZeEa4Hp453LxjQ==";
        private readonly DocumentClient client;


        public RatingRepository()
        {
            client = new DocumentClient(new Uri(EndpointUrl), PrimaryKey);
            GetStarted().Wait();
        }


        private async Task GetStarted()
        {
            await this.client.CreateDatabaseIfNotExistsAsync(new Database { Id = databaseName });
            await this.client.CreateDocumentCollectionIfNotExistsAsync(UriFactory.CreateDatabaseUri(databaseName), new DocumentCollection { Id = "RatingCollection" });

            // ADD THIS PART TO YOUR CODE
            Rating rating = new Rating
            {
                Id = "79c2779e-dd2e-43e8-803d-ecbebed8972c",
                UserId = "cc20a6fb-a91f-4192-874d-132493685376",
                ProductId = "4c25613a-a3c2-4ef3-8e02-9c335eb23204",
                TimeStamp = DateTime.Now,
                LocationName = "Sample ice cream shop",
                RatingValue = 5,
                UserNotes = "I love the subtle notes of orange in this ice cream!"
            };

            await this.CreateDocumentIfNotExists(databaseName, "RatingCollection", rating);

        }


        // ADD THIS PART TO YOUR CODE
        private async Task CreateDocumentIfNotExists(string databaseName, string collectionName, Rating rating)
        {
            try
            {
                await this.client.ReadDocumentAsync(UriFactory.CreateDocumentUri(databaseName, collectionName, rating.Id));
                
            }
            catch (DocumentClientException de)
            {
                if (de.StatusCode == HttpStatusCode.NotFound)
                {
                    await this.client.CreateDocumentAsync(UriFactory.CreateDocumentCollectionUri(databaseName, collectionName), rating);
                    
                }
                else
                {
                    throw;
                }
            }
        }

        public async Task<object> FindAsync()
        {
            try
            {
                var collectionLink = UriFactory.CreateDocumentCollectionUri(databaseName, "Rating");

                RequestOptions options = new RequestOptions();
                options.PartitionKey = new PartitionKey("_id");

                // Set some common query options
                FeedOptions queryOptions = new FeedOptions { MaxItemCount = -1 };

                var docs = await client.ReadDocumentCollectionAsync(collectionLink, options);
                //foreach (var d in docs.TLi)
                //{
                //    Console.WriteLine(d);
                //}

                //var query = this.client.CreateDocumentQuery<IRating>(
                //        UriFactory.CreateDocumentCollectionUri(databaseName, "Rating"), queryOptions)
                //        .ToList();
                var query = client.CreateDocumentCollectionQuery("OpenHackTable08","Rating").ToList();

                //var itr = client.CreateDocumentQuery("Rating", querySpec).AsDocumentQuery();
                //var response = await itr.ExecuteNextAsync<Document>();

                //foreach (var doc in response.AsEnumerable())
                //{
                //    // ...
                //}

                //RequestOptions options = new RequestOptions();
                //options.PartitionKey = new PartitionKey("jay");
                //options.ConsistencyLevel = ConsistencyLevel.Session;
                //return await client.ReplaceDocumentAsync(UriFactory.CreateDocumentUri(DatabaseId, SecurityCollectionId, id), item, options).ConfigureAwait(false);

                return null;

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

        public IRating Find(string ratingId)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<IRating> FindByUser(string userId)
        {
            throw new NotImplementedException();
        }

        #region IDisposable Support
        private bool disposedValue = false; // To detect redundant calls

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    client.Dispose();
                }

                disposedValue = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
        }
        #endregion
    }
}
