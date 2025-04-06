using ApplicationCore.Entities;
using ApplicationCore.Repositories;
using Infrastructure.Data;

namespace Infrastructure.Repositories;

public class GenreRepository: BaseRepository<Genre>, IGenreRepository
{
    public GenreRepository(MovieDbContext movieDbContext) : base(movieDbContext)
    {
    }
}