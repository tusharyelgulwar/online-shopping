using ApplicationCore.Entities;

namespace ApplicationCore.Repositories;

public interface IMovieRepository: IRepository<Movie>
{
    // Additional movie-specific methods
    IEnumerable<Movie> GetTopRatedMovies();
    IEnumerable<Movie> GetMoviesByGenre(int genreId);
    IEnumerable<Movie> GetUpcomingMovies();
}