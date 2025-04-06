using ApplicationCore.Entities;
using ApplicationCore.Models;

namespace ApplicationCore.Contracts.Services;

public interface IAdminService
{
    Task<IEnumerable<TopMovieModel>> GetTopMoviesAsync(DateTime? fromDate = null, DateTime? toDate = null);
    Task<int> CreateMovieAsync(Movie movie);
}