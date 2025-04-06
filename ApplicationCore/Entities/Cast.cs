namespace ApplicationCore.Entities;

public class Cast
{
    public int Id { get; set; }
    public string Gender { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public string ProfilePath { get; set; } = string.Empty;
    public string TmdbUrl { get; set; } = string.Empty;
    
    public ICollection<MovieCast> MovieCasts { get; set; } = new List<MovieCast>();
}