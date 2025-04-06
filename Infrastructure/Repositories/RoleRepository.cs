using ApplicationCore.Entities;
using ApplicationCore.Repositories;
using Infrastructure.Data;

namespace Infrastructure.Repositories;

public class RoleRepository: BaseRepository<Role>, IRoleRepository
{
    public RoleRepository(MovieDbContext movieDbContext) : base(movieDbContext)
    {
    }
}