using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Azure.WebJobs.Host;
using Microsoft.Extensions.Logging;

namespace ServerlessFunctionsApp
{
    public static class HighScoreFunction
    {
        [FunctionName("HighScoreFunction")]
        public static async Task<HttpResponseMessage> Run(
                [HttpTrigger(AuthorizationLevel.Function, "post",
                    Route = "HighScores/player/{nickname}")] HttpRequestMessage req,
                    string nickname, // from route parameter in request URL
                    ILogger log)
        {
            log.LogInformation("C# HTTP trigger function processed a request.");

            // Fetching score from body
            string score = await req.Content.ReadAsStringAsync();

            return int.TryParse(score, out int points) && !String.IsNullOrWhiteSpace(nickname) ?
                    req.CreateResponse(HttpStatusCode.OK, $"{nickname} achieved a score of {points}!") :
                    req.CreateResponse(HttpStatusCode.BadRequest, $"Received invalid nickname and/or score!");
        }
    }
}
