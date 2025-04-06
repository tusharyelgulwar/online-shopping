using ApplicationCore.Entities;
using ApplicationCore.Repositories;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class MovieRepository:BaseRepository<Movie>,IMovieRepository
{
    private readonly MovieDbContext _context;

    public MovieRepository(MovieDbContext movieDbContext) : base(movieDbContext)
    {
        _context = movieDbContext;
    }
    
    public Movie? GetById(int id)
    {
        return _context.Movies
            .Include(m => m.MovieGenres).ThenInclude(mg => mg.Genre)
            .Include(m => m.MovieCasts).ThenInclude(mc => mc.Cast)
            .Include(m => m.Reviews)
            .Include(m => m.Trailers)
            .FirstOrDefault(m => m.Id == id);
    }


    public IEnumerable<Movie> GetTopRatedMovies()
    {
        
        return _context.Movies
            .OrderByDescending(m => m.Revenue)
            .Take(24)
            .ToList();
    }

    public IEnumerable<Movie> GetMoviesByGenre(int genreId)
    {
        return _context.Movies
            .Include(m => m.MovieGenres)
            .Where(m => m.MovieGenres.Any(g => g.GenreId == genreId))
            .ToList();
    }

    public IEnumerable<Movie> GetUpcomingMovies()
    {
        return _context.Movies
            .Where(m => m.ReleaseDate > DateTime.Today)
            .OrderBy(m => m.ReleaseDate)
            .Take(24)
            .ToList();
    }
}