using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Azure.WebJobs.Host;

namespace IceCreamRatingsApi
{
    public static class CreateRating
    {
        [FunctionName("CreateRating")]
        public static async Task<HttpResponseMessage> Run([HttpTrigger(AuthorizationLevel.Function, "get", "post", Route = null)]HttpRequestMessage req, TraceWriter log)
        {
            log.Info("C# HTTP trigger function processed a request.");

            dynamic data = await req.Content.ReadAsAsync<object>();
            Models.Rating rating = new Models.Rating
            {
                UserId = data?.userId,
                ProductId = data?.productId
            };

            if (!rating.IsValid())
            {
                return req.CreateResponse(HttpStatusCode.BadRequest, "UserId or ProductId is missing from data");
            }

            try
            {
                int.Parse(data?.rating);
            }
            catch (CookieException e)
            {
                return req.CreateResponse(HttpStatusCode.BadRequest, "Rating should be integer");
            }

            rating.UserNotes = data?.userNotes;
            rating.Value = data?.rating;

            return req.CreateResponse(HttpStatusCode.OK, "Hello");
        }

        private static string GetProductId(Models.Rating rating)
        {
            //https://serverlessohproduct.trafficmanager.net/api/GetProduct

            return "";
        }

        private static string GetUserId(Models.Rating rating)
        {
            //https://serverlessohuser.trafficmanager.net/api/GetUser

            return "";
        }
    }
}
