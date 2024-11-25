namespace TechEase.Models;

public class Product
{
    public string Name { get; set; } = default!;
    public string? Description { get; set; } = default!;
    public string? Price { get; set; }
    public string? Image { get; set; } = default!;
    
}