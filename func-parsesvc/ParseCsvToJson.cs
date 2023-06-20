using System.Net;
using System.Net.Mime;
using System.Text.RegularExpressions;
using func_parsesvc.Documentation;
using FunctionLib;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Attributes;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Enums;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using static System.Net.Mime.MediaTypeNames;

namespace func_parsesvc
{
    public class ParseCsvToJson
    {
        private readonly ILogger _logger;
        private CSVtoJSONParser _parser;
        public ParseCsvToJson(ILoggerFactory loggerFactory)
        {
            _logger = loggerFactory.CreateLogger<ParseCsvToJson>();
        }


        [OpenApiOperation(operationId: "ParseCSVToJson", tags: new[] { "name" }, Summary = Constants.FUNCTIONSUMMARY, Description = Constants.FUNCTIONDESCRIPTION , Visibility = OpenApiVisibilityType.Important)]
        [OpenApiSecurity("function_key", SecuritySchemeType.ApiKey, Name = "code", In = OpenApiSecurityLocationType.Header)]
        [OpenApiParameter(name: "objectName", In = ParameterLocation.Query, Required = true, Type = typeof(string), Summary = Constants.OBJECTNAMEPARAMSUMMARY, Description = Constants.OBJECTNAMEPARAMDESCRIPTION, Visibility = OpenApiVisibilityType.Important)]
        [OpenApiParameter(name: "csvheaders", In = ParameterLocation.Query, Required = false, Type = typeof(string), Summary = Constants.HEADERPARAMSUMMARY , Description = Constants.HEADERPARAMDESCRIPTION, Visibility = OpenApiVisibilityType.Undefined)]
        [OpenApiParameter(name: "separator", In = ParameterLocation.Query, Required = false, Type = typeof(char), Summary = Constants.SEPARATORPARAMSUMMARY, Description = Constants.SEPARATORPARAMDESCRIPTION, Visibility = OpenApiVisibilityType.Undefined)]
        [OpenApiRequestBody("text/csv", typeof(string))]

        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "json", bodyType: typeof(System.Text.Json.JsonDocument), Summary = "The JSON Response" , Description = "This return the JSON Response of the CSV Payload", Example = typeof(ParametersExample))]
        [OpenApiResponseWithoutBody(statusCode: HttpStatusCode.BadRequest, Description = "The operation was not completed successfully")]
        [Function("parsecsvtojson")]
        public async Task<HttpResponseData> Run([HttpTrigger(AuthorizationLevel.Function,  "post")] HttpRequestData req)
        {
            string query = req.Url.Query;
            var querydict =QueryHelpers.ParseQuery(req.Url.Query);
     

            var objectName = querydict.ContainsKey("objectName") ? querydict.GetValueOrDefault("objectName").FirstOrDefault() : string.Empty;

            var csvheaders = querydict.ContainsKey("csvheaders") ? querydict.GetValueOrDefault("csvheaders").FirstOrDefault() : string.Empty;
            char separator = querydict.ContainsKey("separator") ? Convert.ToChar(querydict.GetValueOrDefault("separator").FirstOrDefault())  : ',';


            _parser =  new CSVtoJSONParser(separator);
            string body = await req.ReadAsStringAsync(System.Text.Encoding.Default);
            var lines = Regex.Split(body, "\r\n|\r|\n");
            var result =  _parser.ParseCSV(lines, objectName, csvheaders.Split(separator));


             foreach (KeyValuePair<string, IEnumerable<string>> header in req.Headers  )
            {
                var headerresult = String.Concat(header.Value.ToArray());
                _logger.LogInformation($"HTTP trigger header {header.Key.ToString() } with value {headerresult}");

            }
                
                    
             var response = req.CreateResponse(HttpStatusCode.OK );

            response.Headers.Add("Content-Type", "application/json; charset=utf-8");

            await response.WriteStringAsync(result);
            return response;
        }
    }
}
