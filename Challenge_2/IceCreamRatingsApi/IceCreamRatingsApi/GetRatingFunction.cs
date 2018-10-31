using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using IceCreamRatingsApi.Models;
using IceCreamRatingsApi.Infrastructure;

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
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "get", Route = null)] HttpRequest req,
            ILogger log)
        {
            log.LogInformation("C# HTTP trigger function processed a request.");

            RatingRepository dbRepos = null;
            string ratingId = req.Query["ratingId"];

            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            dynamic data = JsonConvert.DeserializeObject(requestBody);
            ratingId = ratingId ?? data?.ratingId;

            if (string.IsNullOrWhiteSpace(ratingId))
                new BadRequestObjectResult("Please pass a name on the query string or in the request body");

            try
            {
                dbRepos = new RatingRepository();
                var ratingFound = dbRepos.Find(ratingId);
                if(ratingFound != null)
                    return (ActionResult)new OkObjectResult(ratingFound);
            }
            catch (Exception)
            {

            }
            finally
            {
                //dbRepos?.Close();
            }

            return new NotFoundObjectResult($"The rating cannot be found {ratingId}");
        }
    }
}
