using System.Text;
using System.Text.Json;
using System.Text.RegularExpressions;

namespace BoilerPlateSSR.Queries;

public class HeaderData
{
    public string Text { get; set; }
    public int Level { get; set; }
}

public class ParagraphData
{
    public string Text { get; set; }
}

public class ListData
{
    public List<string> Items { get; set; }
    public string Style { get; set; }
}

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

    public static string GetHtmlFromContent(string content)
    {

        var htmlToGenerate = new StringBuilder();
        var jsonSerializerOptions = new JsonSerializerOptions()
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase
        };
        
        var stuff = JsonDocument.Parse(content);
        var root = stuff.RootElement;

        if (!root.TryGetProperty("blocks", out var blocks)) return htmlToGenerate.ToString();

        if (blocks.ValueKind != JsonValueKind.Array) return htmlToGenerate.ToString();
        
        foreach (var element in blocks.EnumerateArray())
        {
            if (!element.TryGetProperty("type", out var type) ||
                !element.TryGetProperty("data", out var data)) continue;
            
            var typeName = type.GetString();
                        
            if (typeName == "paragraph")
            {
                var paragraphData = data.Deserialize<ParagraphData>(jsonSerializerOptions);
                if (paragraphData != null !)
                {
                    htmlToGenerate.Append($"<p>{paragraphData.Text}</p>");

                }

                continue;
            }
               
            if (typeName == "header")
            {
                var headerData = data.Deserialize<HeaderData>(jsonSerializerOptions);
                if(headerData == null) continue;
                htmlToGenerate.Append($"<h{headerData.Level}>{headerData.Text}</h{headerData.Level}>");
                
                continue;
            }

            if (typeName == "list")
            {
                var listData = data.Deserialize<ListData>(jsonSerializerOptions);
                if (listData == null) continue;
                            
                var listHtml = new StringBuilder();
                listHtml.Append(listData.Style == "unordered" ? "<ul>" : "<ol>");

                foreach (var listItem in listData.Items)
                {
                    listHtml.Append($"<li>{listItem}</li>");
                }
                            
                listHtml.Append(listData.Style == "unordered" ? "</ul>" : "</ol>");

                htmlToGenerate.Append(listHtml);

            }

        }

        return htmlToGenerate.ToString();

    }
}