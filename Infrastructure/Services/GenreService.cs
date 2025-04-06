using ApplicationCore.Contracts.Services;
using ApplicationCore.Models;
using ApplicationCore.Entities;
using Infrastructure.Data;

namespace Infrastructure.Services
{
    public class GenreService : IGenreService
    {
        private readonly MovieDbContext _context;

        public GenreService(MovieDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<GenreModel>> GetAllGenresAsync()
        {
            return _context.Genres
                .OrderBy(g => g.Name)
                .Select(g => new GenreModel
                {
                    Id = g.Id,
                    Name = g.Name
                })
                .ToList();
        }
    }
}