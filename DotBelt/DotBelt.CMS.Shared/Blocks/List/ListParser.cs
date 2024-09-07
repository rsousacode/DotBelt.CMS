using System.Text;
using System.Text.Json;
using DotBelt.CMS.Shared.CMS.Blocks.Parser;

namespace DotBelt.CMS.Shared.CMS.Blocks.List;

[BlockParser("list")]
public class ListParser : StaticHtmlBlockParser
{
    public override string GetHtmlFromJson(JsonElement node)
    {
        
        var listData = node.Deserialize<ListBlock>(WebJsonSerializerConfiguration.Options);
        
        if (listData == null) return string.Empty;
                            
        var listHtml = new StringBuilder();
        listHtml.Append(listData.Style == "unordered" ? "<ul>" : "<ol>");

        foreach (var listItem in listData.Items)
        {
            listHtml.Append($"<li>{listItem}</li>");
        }
                            
        listHtml.Append(listData.Style == "unordered" ? "</ul>" : "</ol>");

        return listHtml.ToString();
    }
}