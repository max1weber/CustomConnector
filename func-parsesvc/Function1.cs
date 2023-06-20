using System.Net;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Extensions.Logging;

namespace func_parsesvc
{
    public class Function1
    {
        private readonly ILogger _logger;

        public Function1(ILoggerFactory loggerFactory)
        {
            _logger = loggerFactory.CreateLogger<Function1>();
        }

        [Function("parse CSV")]
        public HttpResponseData Run([HttpTrigger(AuthorizationLevel.Function,  "post")] HttpRequestData req)
        {
            _logger.LogInformation("C# HTTP trigger function processed a request.");


             foreach (KeyValuePair<string, IEnumerable<string>> header in req.Headers  )
            {
                var headerresult = String.Concat(header.Value.ToArray());
                _logger.LogInformation($"HTTP trigger header {header.Key.ToString() } with value {headerresult}");

            }
                
                    
                    var response = req.CreateResponse(HttpStatusCode.OK);
            response.Headers.Add("Content-Type", "text/plain; charset=utf-8");

            response.WriteString("Welcome to Azure Functions!");

            return response;
        }
    }
}
