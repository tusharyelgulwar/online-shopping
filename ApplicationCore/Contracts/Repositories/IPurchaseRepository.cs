using ApplicationCore.Entities;
using ApplicationCore.Models;

namespace ApplicationCore.Repositories;

public interface IPurchaseRepository: IRepository<Purchase>
{
    Task<IEnumerable<Purchase>> GetPurchasesByUserAsync(int userId);

    Task<bool> IsMoviePurchasedByUserAsync(int movieId, int userId);

    Task<IEnumerable<TopMovieModel>> GetAllMoviesSortedByPurchasesAsync(DateTime? fromDate = null,
        DateTime? toDate = null);
}