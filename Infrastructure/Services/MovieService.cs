using ApplicationCore.Contracts.Services;
using ApplicationCore.Models;
using ApplicationCore.Repositories;

namespace Infrastructure.Services
{
    public class MovieService : IMovieService
    {
        private readonly IMovieRepository _movieRepository;
        private readonly IPurchaseRepository _purchaseRepository;

        public MovieService(IMovieRepository movieRepository, IPurchaseRepository purchaseRepository)
        {
            _movieRepository = movieRepository;
            _purchaseRepository = purchaseRepository;
        }

        

        public async Task<IEnumerable<MovieCardModel>> GetTopMoviesAsync()
        {
            var movies = _movieRepository.GetTopRatedMovies(); 

            return movies
                .Take(24) 
                .Select(m => new MovieCardModel
                {
                    Id = m.Id,
                    Title = m.Title,
                    PosterUrl = string.IsNullOrEmpty(m.PosterUrl) ? "/images/default.jpg" : m.PosterUrl
                });
        }
        
        public Task<IEnumerable<MovieCardModel>> GetTopRatedMoviesAsync()
        {
            var movies = _movieRepository.GetTopRatedMovies().Take(24);

            var result = movies.Select(m => new MovieCardModel
            {
                Id = m.Id,
                Title = m.Title,
                PosterUrl = m.PosterUrl
            });

            return Task.FromResult(result);
        }

        public Task<IEnumerable<MovieCardModel>> GetMoviesByGenreAsync(int genreId)
        {
            var movies = _movieRepository.GetMoviesByGenre(genreId);

            var result = movies.Select(m => new MovieCardModel
            {
                Id = m.Id,
                Title = m.Title,
                PosterUrl = m.PosterUrl
            });

            return Task.FromResult(result);
        }
        
        public async Task<IEnumerable<MovieCardModel>> GetMoviesByGenreSortedAsync(int genreId, string sortBy)
        {
            var movies = _movieRepository.GetMoviesByGenre(genreId);

            movies = sortBy switch
            {
                "releaseDate" => movies.OrderByDescending(m => m.ReleaseDate),
                _ => movies.OrderBy(m => m.Title)
            };

            return await Task.FromResult(
                movies.Select(m => new MovieCardModel
                {
                    Id = m.Id,
                    Title = m.Title,
                    PosterUrl = m.PosterUrl
                })
            );
        }
        
        public async Task<MovieDetailsModel?> GetMovieDetailsAsync(int id, int? userId = null)
        {
            var movie = _movieRepository.GetById(id);
            if (movie == null) return null;

            var genres = movie.MovieGenres?.Select(g => g.Genre.Name).ToList() ?? new List<string>();

            var castList = movie.MovieCasts?.Select(mc => new CastModel
            {
                Id = mc.CastId,
                Name = mc.Cast?.Name ?? "Unknown",
                ProfilePath = string.IsNullOrEmpty(mc.Cast?.ProfilePath) ? "/images/default-cast.jpg" : mc.Cast.ProfilePath,
                Character = mc.Character ?? "Unknown"
            }).ToList() ?? new List<CastModel>();

            var trailerList = movie.Trailers?.Select(t => new TrailerModel
            {
                Name = t.Name,
                Url = t.TrailerUrl
            }).ToList() ?? new List<TrailerModel>();

            var rating = movie.Reviews.Any()
                ? decimal.Round(movie.Reviews.Average(r => r.Rating), 1)
                : 3.5m;
            
            bool isPurchased = false;
            if (userId.HasValue)
            {
                isPurchased = await _purchaseRepository.IsMoviePurchasedByUserAsync(id, userId.Value);
            }

            return new MovieDetailsModel
            {
                Id = movie.Id,
                Title = movie.Title ?? "Untitled",
                Overview = movie.Overview,
                PosterUrl = string.IsNullOrEmpty(movie.PosterUrl) ? "/images/default.jpg" : movie.PosterUrl!,
                BackdropUrl = string.IsNullOrEmpty(movie.BackdropUrl) ? "/images/default-bg.jpg" : movie.BackdropUrl,
                TagLine = movie.TagLine,
                ReleaseDate = movie.ReleaseDate.ToString("MMM dd, yyyy"),
                Runtime = $"{movie.Runtime} min",
                Genres = genres,
                Rating = rating,
                Price = movie.Price ?? 9.99m,
                Revenue = movie.Revenue,
                Budget = movie.Budget,
                ImdbUrl = movie.ImdbUrl,
                Casts = castList,
                Trailers = trailerList,
                IsPurchased = isPurchased
            };
        }





    }
    
    
}