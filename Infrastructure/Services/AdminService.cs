using ApplicationCore.Contracts.Services;
using ApplicationCore.Entities;
using ApplicationCore.Models;
using ApplicationCore.Repositories;

namespace Infrastructure.Services;

public class AdminService : IAdminService
{
    private readonly IMovieRepository _movieRepository;
    private readonly IPurchaseRepository _purchaseRepository;

    public AdminService(IMovieRepository movieRepository, IPurchaseRepository purchaseRepository)
    {
        _movieRepository = movieRepository;
        _purchaseRepository = purchaseRepository;
    }

    public async Task<IEnumerable<TopMovieModel>> GetTopMoviesAsync(DateTime? fromDate = null, DateTime? toDate = null)
    {
        return await _purchaseRepository.GetAllMoviesSortedByPurchasesAsync(fromDate, toDate);
    }



    public async Task<int> CreateMovieAsync(Movie movie)
    {
        return await Task.FromResult(_movieRepository.Insert(movie));
    }
}
