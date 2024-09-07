using System.Text;
using System.Text.Json;
using System.Text.RegularExpressions;

namespace DotBelt.Queries;

public static class PostHelpers
{
    public static string SanitizePermalink(string? permalink)
    {
        if(permalink == null) return string.Empty;
        
        // Replace spaces with hyphens
        permalink = permalink.Replace(" ", "-");

        // Remove any characters that are not letters, numbers, or hyphens
        permalink = Regex.Replace(permalink, "[^a-zA-Z0-9-/]", "");

        // Convert the permalink to lowercase
        permalink = permalink.ToLowerInvariant();

        return permalink;
    }


}