using System.Globalization;
using System.Text;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi;
using Microsoft.OpenApi.Extensions;
using Microsoft.OpenApi.Models;
using Microsoft.OpenApi.Writers;
using Swashbuckle.AspNetCore.Swagger;

namespace BoilerPlateSSR.Swagger;

public class SwaggerExtensions
{
    public static async Task<string> GetJsonSchema(IServiceProvider services)
    {
        var swaggerProvider = services.GetService<IAsyncSwaggerProvider>();
        
        if (swaggerProvider == null)
        {
            throw new InvalidOperationException($"No service {nameof(IAsyncSwaggerProvider)} found.");
        }
        

        OpenApiDocument document = await swaggerProvider.GetSwaggerAsync("v1", null, null);

        using (StringWriter textWriter = new StringWriter((IFormatProvider) CultureInfo.InvariantCulture))
        {
            OpenApiJsonWriter writer = new OpenApiJsonWriter((TextWriter) textWriter);
            document.SerializeAsV3((IOpenApiWriter) writer);
            var result = textWriter.ToString();
            return result;
        }

    }
}