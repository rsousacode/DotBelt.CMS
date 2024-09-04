namespace DotBelt.CMS.Shared.CMS.Blocks.Parser;

public static class BlockParserLoader
{
    public static Dictionary<string, StaticHtmlBlockParser> GetStaticHtmlParsers() 
    {
        var result = new Dictionary<string, StaticHtmlBlockParser>();
        
        var blockParserTypes = GetBlockParsers();
            
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

    private static IEnumerable<Type> GetBlockParsers()
    {
        var assemblies = AppDomain.CurrentDomain.GetAssemblies();
        var blockParsers = new List<Type>();

        foreach (var assembly in assemblies)
        {
            var types = assembly.GetTypes();

            foreach (var type in types)
            {
                var attributes = type.GetCustomAttributes(typeof(BlockParserAttribute), false);
                   
                if (attributes.Length > 0)
                {
                    blockParsers.Add(type);
                }
            }
        }

        return blockParsers;
    }
}