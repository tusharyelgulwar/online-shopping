namespace ApplicationCore.Entities;

public class Trailer
{
    public int Id { get; set; }

    public int MovieId { get; set; }
    public Movie Movie { get; set; }

    public string Name { get; set; } = string.Empty;
    public string TrailerUrl { get; set; } = string.Empty;
}
