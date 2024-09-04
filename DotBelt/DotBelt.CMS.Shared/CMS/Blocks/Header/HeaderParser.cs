using System.Text.Json;
using DotBelt.CMS.Shared.CMS.Blocks.Parser;

namespace DotBelt.CMS.Shared.CMS.Blocks.Header;

public class HeaderParser : StaticHtmlBlockParser
{
    public override string GetHtmlFromJson(JsonElement node)
    {
        var headerData = node.Deserialize<HeaderBlock>(WebJsonSerializerConfiguration.Options);
        
        if (headerData == null) return string.Empty;
        
        return $"<h{headerData.Level}>{headerData.Text}</h{headerData.Level}>";

    }
}