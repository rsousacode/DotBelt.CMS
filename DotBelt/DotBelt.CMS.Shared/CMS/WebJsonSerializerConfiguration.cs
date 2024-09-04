using System.Text.Json;

namespace DotBelt.CMS.Shared.CMS;

public static class WebJsonSerializerConfiguration
{
    public static JsonSerializerOptions Options = new JsonSerializerOptions(JsonSerializerDefaults.Web);
}