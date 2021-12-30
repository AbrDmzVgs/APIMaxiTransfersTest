using Microsoft.OpenApi.Any;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace APIMaxiTransfersTest.Filters
{
    public class RequestHeaderFilter : IOperationFilter
    {
        void IOperationFilter.Apply(OpenApiOperation operation, OperationFilterContext context)
        {
            operation.Parameters.Add(new OpenApiParameter()
            {
                Name = "UserName",
                Required = true,
                In = ParameterLocation.Header,
                Schema = new OpenApiSchema
                {
                    Type = "string",
                    Default = new OpenApiString("MAXITEST")
                }
            });

            operation.Security.Add(new OpenApiSecurityRequirement
            {
                {
                    new OpenApiSecurityScheme
                    {
                        Name = "Authorization",
                        In = ParameterLocation.Header,
                        BearerFormat = "Bearer token",

                        Reference = new OpenApiReference
                        {
                            Type = ReferenceType.SecurityScheme,
                            Id = "Bearer"
                        }
                    },new string[]{ }
                }
            });
        }
    }
}
