namespace ApplicationCore.Models;

public class TopMovieModel
{
    public int MovieId { get; set; }
    public string Title { get; set; } = string.Empty;
    public string PosterUrl { get; set; } = string.Empty;
    public int TotalPurchases { get; set; }
}
