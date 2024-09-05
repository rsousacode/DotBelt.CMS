namespace BoilerPlateSSR.Internal;

public static class Reflection
{
    public static IEnumerable<Type> GetTypesUsingAttribute<T>() where T : Attribute
    {
        var assemblies = AppDomain.CurrentDomain.GetAssemblies();
        var blockParsers = new List<Type>();

        foreach (var assembly in assemblies)
        {
            var types = assembly.GetTypes();

            foreach (var type in types)
            {
                var attributes = type.GetCustomAttributes(typeof(T), false);
                   
                if (attributes.Length > 0)
                {
                    blockParsers.Add(type);
                }
            }
        }

        return blockParsers;
    }
}