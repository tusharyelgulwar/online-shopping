namespace ApplicationCore.Models;

public class MovieCardModel
{
    public int Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string PosterUrl { get; set; } = string.Empty;
    
    // Add purchase details:
    public DateTime PurchaseDate { get; set; }
    public decimal TotalPrice { get; set; }
    public Guid PurchaseNumber { get; set; }
}