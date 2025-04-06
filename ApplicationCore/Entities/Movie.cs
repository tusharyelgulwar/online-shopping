namespace ApplicationCore.Entities;

public class Movie
{
    public int Id { get; set; }
    public string BackdropUrl { get; set; } = string.Empty;
    public decimal Budget { get; set; }
    public string? CreatedBy { get; set; } = string.Empty;
    public DateTime? CreatedDate { get; set; }
    public string ImdbUrl { get; set; } = string.Empty;
    public string OriginalLanguage { get; set; } = string.Empty;
    public string Overview { get; set; } = string.Empty;
    public string? PosterUrl { get; set; }
    public decimal? Price { get; set; }
    public DateTime ReleaseDate { get; set; }
    public decimal Revenue { get; set; }
    public int Runtime { get; set; }
    public string TagLine { get; set; } = string.Empty;
    public string? Title { get; set; }
    public string TmdbUrl { get; set; } = string.Empty;
    
    
    // Navigation properties
    public ICollection<MovieGenre> MovieGenres { get; set; } = new List<MovieGenre>();
    public ICollection<MovieCast> MovieCasts { get; set; } = new List<MovieCast>();
    public ICollection<Review> Reviews { get; set; } = new List<Review>();
    public ICollection<Purchase> Purchases { get; set; } = new List<Purchase>();
    public ICollection<Favorite> Favorites { get; set; } = new List<Favorite>();
    public ICollection<Trailer> Trailers { get; set; } = new List<Trailer>();
}