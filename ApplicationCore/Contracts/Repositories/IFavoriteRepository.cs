using ApplicationCore.Entities;
using ApplicationCore.Repositories;

namespace ApplicationCore.Contracts.Repositories
{
    public interface IFavoriteRepository : IRepository<Favorite>
    {
        Task<IEnumerable<Movie>> GetFavoritesByUser(int userId);
    }
}