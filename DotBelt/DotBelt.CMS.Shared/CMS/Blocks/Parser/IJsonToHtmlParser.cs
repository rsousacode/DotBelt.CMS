using System.Text.Json;

namespace DotBelt.CMS.Shared.CMS.Blocks.Parser;

public interface IJsonToHtmlParser
{
    public string GetHtmlFromJson(JsonElement node);
}