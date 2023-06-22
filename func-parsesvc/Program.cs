using Microsoft.Azure.Functions.Worker.Extensions.OpenApi.Extensions;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Abstractions;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Configurations;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Net;

var host = new HostBuilder()
    
    .ConfigureFunctionsWorkerDefaults(worker => worker.UseNewtonsoftJson())
    .ConfigureServices(services =>
    {
        //_ = services.AddSingleton<IOpenApiHttpTriggerAuthorization>(_ =>
        //{
        //    var auth = new OpenApiHttpTriggerAuthorization(req =>
        //    {
        //        var result = default(OpenApiAuthorizationResult);

        //        var authtoken = (string)req.Headers["Authorization"];
        //        if (string.IsNullOrWhiteSpace(authtoken))
        //        {
        //            result = new OpenApiAuthorizationResult()
        //            {
        //                StatusCode = HttpStatusCode.Unauthorized,
        //                ContentType = "text/plain",
        //                Payload = "Unauthorized",
        //            };

        //            return Task.FromResult(result);
        //        }

        //        return Task.FromResult(result);
        //    });

        //    return auth;
        //});
    })
    .Build();
host.Run();
