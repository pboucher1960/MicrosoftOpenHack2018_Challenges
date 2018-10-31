using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using Microsoft.Azure.Documents;
using Microsoft.Azure.Documents.Client;
using Newtonsoft.Json;



namespace Infrastructure
{
    public class Rating
    {
        private const string databaseName = "OpenHackTable08";
        private const string EndpointUrl = "https://openhacktable08.documents.azure.com:10255";
        private const string PrimaryKey = "VZFwRgdsJhhlPXgeQVCmtgTBv1W09VICD91MYUXbIyVEEQRrVYvmOKasaZW6Pax1hsKfw7XRZeEa4Hp453LxjQ==";
        private DocumentClient client;


        public Rating()
        {

        }

        public IEnumerable<IRating> Find()
        {

            // Set some common query options
            FeedOptions queryOptions = new FeedOptions { MaxItemCount = -1 };

            // Here we find the Andersen family via its LastName
            IEnumerable<IRating> query = this.client.CreateDocumentQuery<IRating>(
                    UriFactory.CreateDocumentCollectionUri(databaseName, "Rating"), queryOptions)
                    .ToList();

            return query;
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
