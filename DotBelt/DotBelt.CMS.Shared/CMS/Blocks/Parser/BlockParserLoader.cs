using BoilerPlateSSR.Internal;

namespace DotBelt.CMS.Shared.CMS.Blocks.Parser;

public static class BlockParserLoader
{
    public static Dictionary<string, StaticHtmlBlockParser> GetStaticHtmlParsers() 
    {
        var result = new Dictionary<string, StaticHtmlBlockParser>();
        
        var blockParserTypes = Reflection.GetTypesUsingAttribute<BlockParserAttribute>();
            
        foreach (var type in blockParserTypes)
        {
            if(Activator.CreateInstance(type) is not StaticHtmlBlockParser parser) 
            {
                throw new InvalidOperationException("Cannot create instance of type " + type.FullName + 
                                                    ". Is it a valid StaticHtmlBlockParser?");
            }
            
            result.Add(parser.Type, parser);
        }

        return result;
    }

}