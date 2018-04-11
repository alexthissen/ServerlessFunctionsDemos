using System.Linq;
using System.Net;
using System.Net.Http;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Azure.WebJobs.Host;
using System.Threading.Tasks;
using System;
using System.Text;

namespace ServerlessFunctionApp
{
    public static class DumpHeadersFunction
    {
        [FunctionName("DumpHeadersFunction")]
        public static HttpResponseMessage Run([HttpTrigger] HttpRequestMessage req, TraceWriter log)
        {
            log.Info("C# HTTP trigger function processed a request.");
            StringBuilder builder = new StringBuilder();
            foreach (var header in req.Headers)
            {
                builder.AppendFormat("{0}='{1}',", header.Key, String.Concat(header.Value));
            }

            return req.CreateResponse(HttpStatusCode.OK, builder.ToString());
        }
    }
}
