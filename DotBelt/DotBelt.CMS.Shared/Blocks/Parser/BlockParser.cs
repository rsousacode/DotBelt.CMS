using System.Reflection;
using System.Text;
using System.Text.Json;

namespace DotBelt.CMS.Shared.CMS.Blocks.Parser;


[RegisterSingleton]
public class BlockParser
{
    private Dictionary<string, StaticHtmlBlockParser> _parsers { get; }

    public BlockParser()
    {
        _parsers = BlockParserLoader.GetStaticHtmlParsers();
    }
    
    public string GetHtmlFromContent(string content)
    {

        var htmlToGenerate = new StringBuilder();

        var stuff = JsonDocument.Parse(content);
        var root = stuff.RootElement;

        if (!root.TryGetProperty("blocks", out var blocks)) return htmlToGenerate.ToString();

        if (blocks.ValueKind != JsonValueKind.Array) return htmlToGenerate.ToString();
        
        foreach (var element in blocks.EnumerateArray())
        {
            if (!element.TryGetProperty("type", out var type) ||
                !element.TryGetProperty("data", out var data)) continue;
            
            var typeName = type.GetString();

            var html = GetStaticHtmlFromJsonBlock(typeName!, data);
            
            htmlToGenerate.Append(html);

        }

        return htmlToGenerate.ToString();

    }
    
    public string GetStaticHtmlFromJsonBlock(string type, JsonElement element) 
    {
        if(!_parsers.TryGetValue(type, out var parser)) 
        {
            throw new InvalidOperationException($"There is no StaticHtmlBlockParser parser for '{type}'");
        }
        
        return parser.GetHtmlFromJson(element);
    }
}