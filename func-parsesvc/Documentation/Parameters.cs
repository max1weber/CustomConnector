using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Abstractions;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Attributes;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Resolvers;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace func_parsesvc.Documentation
{
    [OpenApiExample(typeof(ParametersExample))]
    public class Parameters
    {
        /// <summary>The id of the customer in the context. This is also called payer, sub_account_id.</summary>
        [OpenApiProperty(Description ="The Object name supplied by the Request.")]
        [Newtonsoft.Json.JsonProperty("objectname", Required = Newtonsoft.Json.Required.Always)]
        [System.ComponentModel.DataAnnotations.Required(AllowEmptyStrings = true)]
        public string objectname { get; set; }

       
    }

    public class ParametersExample : OpenApiExample<Parameters>
    {
        public override IOpenApiExample<Parameters> Build(NamingStrategy namingStrategy = null)
        {
            this.Examples.Add(
                OpenApiExampleResolver.Resolve(
                    "ParametersExample",
                    new Parameters()
                    {
                        objectname = "TestObjectName",
                        
                    },
                    namingStrategy
                ));

            return this;
        }
    }
}
