namespace ApplicationCore.Models;

public class MovieDetailsModel
{
    public int Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Overview { get; set; } = string.Empty;
    public string PosterUrl { get; set; } = string.Empty;
    public string BackdropUrl { get; set; } = string.Empty;
    public string TagLine { get; set; } = string.Empty;
    public string ReleaseDate { get; set; }
    public string Runtime { get; set; } = string.Empty;
    public decimal Rating { get; set; }
    public decimal Price { get; set; }
    public decimal Revenue { get; set; }
    public decimal Budget { get; set; }
    public string ImdbUrl { get; set; } = string.Empty;
    public List<string> Genres { get; set; } = new();
    public List<CastModel> Casts { get; set; } = new();
    public List<TrailerModel> Trailers { get; set; } = new();
    
    public bool IsPurchased { get; set; }

}