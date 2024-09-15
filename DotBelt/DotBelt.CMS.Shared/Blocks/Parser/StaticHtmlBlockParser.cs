using System.Text.Json;

namespace DotBelt.CMS.Shared.CMS.Blocks.Parser;

public abstract class StaticHtmlBlockParser : IJsonToHtmlParser
{
    public readonly string Type;

    protected StaticHtmlBlockParser(string type)
    {
        Type = type;
    }

    public abstract string GetHtmlFromJson(JsonElement node);
}