using Microsoft.OpenApi.Models;

namespace Inventory.API.Configs
{
    public class SwaggerConfig
    {
        public static OpenApiInfo GetOpenApiInfo(string env)
        {
            return new OpenApiInfo
            {
                Version = "v1",  // Use "v1" or a suitable version identifier
                Title = "Inventory Service",
                Description = $"Inventory REST {env} Service (.NET {Environment.Version})",
                Contact = new OpenApiContact
                {
                    Name = "Swagger Codegen Contributors",
                    Url = new Uri("https://github.com/swagger-api/swagger-codegen")
                },
                TermsOfService = new Uri("http://swagger.io/terms/")
            };
        }
    }
}
