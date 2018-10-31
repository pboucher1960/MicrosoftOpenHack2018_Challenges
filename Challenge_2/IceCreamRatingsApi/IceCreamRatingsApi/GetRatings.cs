using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Azure.WebJobs.Host;

namespace IceCreamRatingsApi
{
    public static class GetRatings
    {
        [FunctionName("GetRatings")]
        public static async Task<HttpResponseMessage> Run([HttpTrigger(AuthorizationLevel.Function, "get", Route = null)]HttpRequestMessage req, TraceWriter log)
        {
             // parse query parameter
            string userId = req.GetQueryNameValuePairs()
                .FirstOrDefault(q => string.Compare(q.Key, "userId", true) == 0)
                .Value;

            if (userId == null)
            {
                // Get request body
                dynamic data = await req.Content.ReadAsAsync<object>();
                userId = data?.userId;
            }

            return userId == null
                ? req.CreateResponse(HttpStatusCode.BadRequest, "Please pass a user identifier on the query string or in the request body")
                : req.CreateResponse(HttpStatusCode.OK, "Hello " + userId);
        }
    }
}
