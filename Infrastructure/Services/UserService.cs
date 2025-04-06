using ApplicationCore.Contracts.Services;
using ApplicationCore.Contracts.Repositories;
using ApplicationCore.Models;
using ApplicationCore.Repositories;

namespace Infrastructure.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IPurchaseRepository _purchaseRepository;
        private readonly IFavoriteRepository _favoriteRepository;

        public UserService(
            IUserRepository userRepository,
            IPurchaseRepository purchaseRepository,
            IFavoriteRepository favoriteRepository)
        {
            _userRepository = userRepository;
            _purchaseRepository = purchaseRepository;
            _favoriteRepository = favoriteRepository;
        }

        public async Task<IEnumerable<PurchaseMovieCardModel>> GetPurchasedMoviesAsync(int userId)
        {
            var purchases = await _purchaseRepository.GetPurchasesByUserAsync(userId); // new method

            return purchases.Select(p => new PurchaseMovieCardModel
            {
                Id = p.Movie.Id,
                Title = p.Movie.Title,
                PosterUrl = p.Movie.PosterUrl ?? "/images/default.jpg",
                PurchaseDateTime = p.PurchaseDateTime,
                TotalPrice = p.TotalPrice,
                PurchaseNumber = p.PurchaseNumber
            });
        }


        public async Task<IEnumerable<MovieCardModel>> GetFavoriteMoviesAsync(int userId)
        {
            var movies = await _favoriteRepository.GetFavoritesByUser(userId);

            return movies.Select(m => new MovieCardModel
            {
                Id = m.Id,
                Title = m.Title,
                PosterUrl = m.PosterUrl
            });
        }

        public async Task<UserProfileModel> GetUserProfileAsync(int userId)
        {
            var user = _userRepository.GetById(userId);

            return new UserProfileModel
            {
                Id = user.Id,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
                DateOfBirth = user.DateOfBirth
            };
        }
    }
}