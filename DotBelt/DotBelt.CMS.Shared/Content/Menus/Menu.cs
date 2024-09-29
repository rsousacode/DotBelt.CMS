namespace DotBelt.CMS.Shared.CMS.Menus;

public class Menu
{
    public int Id { get; set; }
    public required string Title { get; set; }
    public ICollection<Menu> Children { get; set; } = null!;
    public Menu? Parent { get; set; }
    public int? ParentId { get; set; }
    public string? FullUrl { get; set; }
    
    public Post? Post { get; set; } 
    public int? PostId { get; set; }
    

}