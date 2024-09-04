using System.Text;
using System.Text.Json;
using DotBelt.CMS.Shared.CMS.Blocks.Parser;

namespace DotBelt.CMS.Shared.CMS.Blocks;

[BlockParser("paragraph")]
public class ParagraphParser : StaticHtmlBlockParser
{
    public override string GetHtmlFromJson(JsonElement node)
    {
        var builder = new StringBuilder();
        
        var paragraphData = node.Deserialize<ParagraphBlock>(WebJsonSerializerConfiguration.Options);
        
        if (paragraphData != null !)
        {
            builder.Append($"<p>{paragraphData.Text}</p>");
        }
        
        return builder.ToString();
       
    }
}