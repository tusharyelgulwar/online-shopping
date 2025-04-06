namespace ApplicationCore.Contracts.Services;
using ApplicationCore.Models;
using ApplicationCore.Entities;

public interface IMovieService
{
    // Model-based results (for display)
    Task<IEnumerable<MovieCardModel>> GetTopMoviesAsync();
    Task<IEnumerable<MovieCardModel>> GetTopRatedMoviesAsync();
    Task<IEnumerable<MovieCardModel>> GetMoviesByGenreAsync(int genreId);
    
    Task<IEnumerable<MovieCardModel>> GetMoviesByGenreSortedAsync(int genreId, string sortBy);

    Task<MovieDetailsModel?> GetMovieDetailsAsync(int id, int? userId = null);


}