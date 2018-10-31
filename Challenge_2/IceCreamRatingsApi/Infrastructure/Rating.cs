using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.Azure.Documents;
using Microsoft.Azure.Documents.Client;
using Newtonsoft.Json;



namespace Infrastructure
{
    public class Rating
    {
        private const string databaseName = "OpenHackTable08";
        private const string EndpointUrl = "https://openhacktable08.documents.azure.com:443";
        private const string PrimaryKey = "VZFwRgdsJhhlPXgeQVCmtgTBv1W09VICD91MYUXbIyVEEQRrVYvmOKasaZW6Pax1hsKfw7XRZeEa4Hp453LxjQ==";
        private DocumentClient client;


        public Rating()
        {
            client = new DocumentClient(new Uri(EndpointUrl), PrimaryKey);
            GetStarted().Wait();
        }


        private async Task GetStarted()
        {
            await this.client.CreateDatabaseIfNotExistsAsync(new Database { Id = databaseName });
            await this.client.CreateDocumentCollectionIfNotExistsAsync(UriFactory.CreateDatabaseUri(databaseName), new DocumentCollection { Id = "RatingCollection" });
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
    }
}
