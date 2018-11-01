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
            Models.UserRating rating = new Models.UserRating
            {
                UserId = data?.userId,
                ProductId = data?.productId
            };

            if (!rating.IsValid())
            {
                return new BadRequestObjectResult("UserId or ProductId is missing from data");
            }

            Models.Product product = await GetProductId(rating);

            if (product == null)
            {
                return new BadRequestObjectResult("Invalid productId: " + rating.ProductId);
            }

            Models.User user = await GetUserId(rating);

            if (user == null)
            {
                return new BadRequestObjectResult("Invalid userId: " + rating.UserId);
            }

            if (data?.rating > 5 || data?.rating < 0)
            {
                return new BadRequestObjectResult("Rating should be between 0 and 5");
            }

            rating.UserNotes = data?.userNotes;
            rating.Rating = data?.rating;

            Infrastructure.UserRatingManager.AddAsync(rating).Wait();

            return new OkObjectResult("Rating created");
        }

        public static async Task<Models.Product> GetProductId(Models.UserRating rating)
        {
            HttpResponseMessage response = await CallApi("GetProduct?productId=" + rating.ProductId);

            if (response.IsSuccessStatusCode)
            {
                Models.Product product = await response.Content.ReadAsAsync<Models.Product>();

                return product;
            }

            return null;
        }

        public static async Task<Models.User> GetUserId(Models.UserRating rating)
        {
            HttpResponseMessage response = await CallApi("GetUser?userId=" + rating.UserId);

            if (response.IsSuccessStatusCode)
            {
                Models.User user = await response.Content.ReadAsAsync<Models.User>();

                return user;
            }

            return null;
        }

        private static async Task<HttpResponseMessage> CallApi(string param)
        {
            const string baseUrl = "https://serverlessohproduct.trafficmanager.net/api/";
            HttpClient client = new HttpClient();
            string url = baseUrl + param;
            client.BaseAddress = new Uri(url);
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            HttpResponseMessage response = await client.GetAsync(url);

            return response;
        }
    }
}
