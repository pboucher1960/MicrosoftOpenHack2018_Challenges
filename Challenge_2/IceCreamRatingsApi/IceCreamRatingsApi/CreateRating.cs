using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System.Net.Http;
using System.Net.Http.Headers;

namespace IceCreamRatingsApi
{
    public static class CreateRating
    {
        [FunctionName("CreateRating")]
        public static async Task<IActionResult> Run([HttpTrigger(AuthorizationLevel.Function, "get", "post", Route = null)] HttpRequest req, ILogger log)
        {
            log.LogInformation("C# HTTP trigger function processed a request.");

            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            dynamic data = JsonConvert.DeserializeObject(requestBody);
            Models.Rating rating = new Models.Rating
            {
                UserId = data?.userId,
                ProductId = data?.productId
            };

            if (!rating.IsValid())
            {
                return new BadRequestObjectResult("UserId or ProductId is missing from data");
            }

            try
            {
                int.Parse(data?.rating);
            }
            catch (Exception e)
            {
                return new BadRequestObjectResult("Rating should be integer");
            }

            rating.UserNotes = data?.userNotes;
            rating.Value = data?.rating;

            return new OkObjectResult("Created");
        }

        public static async Task<Models.Product> GetProductId(Models.Rating rating)
        {
            HttpResponseMessage response = await CallApi("https://serverlessohproduct.trafficmanager.net/api/GetProduct?productId=" + rating.ProductId);

            if (response.IsSuccessStatusCode)
            {
                Models.Product product = await response.Content.ReadAsAsync<Models.Product>();

                return product;
            }

            return null;
        }

        public static string GetUserId(Models.Rating rating)
        {
            //https://serverlessohuser.trafficmanager.net/api/GetUser?userId=rating.UserId

            return "";
        }

        private static async Task<HttpResponseMessage> CallApi(string url)
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(url);
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            HttpResponseMessage response = await client.GetAsync(url);

            return response;
        }
    }
}
