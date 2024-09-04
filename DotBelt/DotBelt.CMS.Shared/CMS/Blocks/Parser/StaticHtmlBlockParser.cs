using System.Reflection;
using System.Text.Json;
using DotBelt.CMS.Shared.CMS.Blocks.Parser;

namespace DotBelt.CMS.Shared.CMS.Blocks.Parser;

[AttributeUsage(AttributeTargets.Class)]
public class BlockParserAttribute : Attribute
{
    public string Type;
    
    public BlockParserAttribute(string type)
    {
        Type = type;
    }
}


public abstract class StaticHtmlBlockParser : IJsonToHtmlParser
{
    public readonly string Type;

    protected StaticHtmlBlockParser()
    {
        var attribute = GetType().GetCustomAttribute(typeof(BlockParserAttribute));
        
        if ( attribute is not BlockParserAttribute attributeData) 
        {
            throw new InvalidOperationException($"BlockParserAttribute must be set in ${GetType().Name}");
        }
        
        if (string.IsNullOrEmpty(attributeData.Type)) 
        {
            throw new InvalidOperationException($"Type in BlockParserAttribute must not be null or empty");
        }
        
        Type = attributeData.Type;
    }

    public abstract string GetHtmlFromJson(JsonElement node);
}