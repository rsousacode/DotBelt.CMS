namespace DotBelt.QueriesMutations.Posts;

public record Edit_PostRequest(string? Title, string? Description, string? RelativeUrl, string Content);
