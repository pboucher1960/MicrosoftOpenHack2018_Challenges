using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Azure.WebJobs.Host;

namespace IceCreamRatingsApi
{
    public static class GetRatingFunction
    {
        /// <summary>
        /// Example of the Respnse requsest:
        /// 
        /// 
        /// {
        ///"id": "79c2779e-dd2e-43e8-803d-ecbebed8972c",
        ///"userId": "cc20a6fb-a91f-4192-874d-132493685376",
        ///"productId": "4c25613a-a3c2-4ef3-8e02-9c335eb23204",
        ///"timestamp": "2018-05-21 21:27:47Z",
        ///"locationName": "Sample ice cream shop",
        ///"rating": 5,
        ///"userNotes": "I love the subtle notes of orange in this ice cream!"
        ///}
        /// </summary>
        /// <param name="req"></param>
        /// <param name="log"></param>
        /// <returns></returns>
        [FunctionName("GetRatingFunction")]
        public static async Task<HttpResponseMessage> Run([HttpTrigger(AuthorizationLevel.Function, "get", Route = null)]HttpRequestMessage req, TraceWriter log)
        {
            log.Info("C# HTTP trigger function processed a request.");

            // parse query parameter
            string ratingId = req.GetQueryNameValuePairs()
                .FirstOrDefault(q => string.Compare(q.Key, "ratingId", true) == 0)
                .Value;

            //if (name == null)
            //{
            //    // Get request body
            //    dynamic data = await req.Content.ReadAsAsync<object>();
            //    name = data?.name;
            //}

            return ratingId == null
                ? req.CreateResponse(HttpStatusCode.BadRequest, "Please pass a name on the query string or in the request body")
                : req.CreateResponse(HttpStatusCode.OK, new
                {
                    id = "79c2779e-dd2e-43e8-803d-ecbebed8972c",
                    userId = "cc20a6fb-a91f-4192-874d-132493685376",
                    productId = "4c25613a-a3c2-4ef3-8e02-9c335eb23204",
                    timestamp = "2018-05-21 21:27:47Z",
                    locationName = "Sample ice cream shop",
                    rating = 5,
                    userNotes = "I love the subtle notes of orange in this ice cream!"
                });
        }
    }
}
