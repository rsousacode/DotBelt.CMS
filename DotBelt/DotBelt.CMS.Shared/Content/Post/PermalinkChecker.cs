using System.Text.RegularExpressions;
using Microsoft.EntityFrameworkCore;

namespace DotBelt.CMS.Shared.Content.Post;

[RegisterScoped]
public class PermalinkChecker : IDisposable, IAsyncDisposable
{
    private readonly ApplicationDbContext _context;

    private string SanitizePermalink(string? permalink)
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

    public async Task<string> CheckPermalink(string relativeUrl, int? postId = null, CancellationToken ct = default)
    {
        relativeUrl = SanitizePermalink(relativeUrl);

        var permalinkExists = await _context
            .Posts
            .Where(x => x.Id != postId)
            .AnyAsync(x => x.RelativeUrl == relativeUrl, cancellationToken: ct);

        if (permalinkExists)
        {
            if (EndsWithDashAndNumber(relativeUrl, out var number, out var modifiedString))
            {
                var newPermalink = $"{modifiedString}-{number + 1}";
                return await CheckPermalink(newPermalink, postId, ct);
            }
            
            return await CheckPermalink($"{relativeUrl}-2", postId, ct);
            
        }

        return relativeUrl;
    }

    public async Task<string> GetFullUrl(string relativeUrl, CancellationToken ct)
    {
        var tenantFullUrl = await _context
            .Tenants
            .Select(x => x.FullUrl)
            .FirstOrDefaultAsync(cancellationToken: ct);

        return $"{tenantFullUrl}/{relativeUrl}";
    }
    
    static bool EndsWithDashAndNumber(string input, out int number, out string modifiedString)
    {
        // Initialize out parameters
        number = 0;
        modifiedString = input;
        
        // Regular expression to match a dash followed by one or more digits at the end of the string
        string pattern = @"-(\d+)$";
        Match match = Regex.Match(input, pattern);
        
        if (match.Success)
        {
            // Parse and return the captured number
            number = int.Parse(match.Groups[1].Value);
            // Remove the matched part from the original string
            modifiedString = input.Substring(0, match.Index);
            return true;
        }

        return false;
    }
    
    public PermalinkChecker(ApplicationDbContext context)
    {
        _context = context;
    }

    public void Dispose()
    {
        _context.Dispose();
    }

    public async ValueTask DisposeAsync()
    {
        await _context.DisposeAsync();
    }
}