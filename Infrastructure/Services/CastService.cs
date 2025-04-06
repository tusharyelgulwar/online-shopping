using ApplicationCore.Contracts.Services;
using ApplicationCore.Entities;
using ApplicationCore.Repositories;

namespace Infrastructure.Services;

public class CastService : ICastService
{
    private readonly ICastRepository _castRepository;

    public CastService(ICastRepository castRepository)
    {
        _castRepository = castRepository;
    }

    public async Task<Cast?> GetCastByIdAsync(int id)
    {
        return await Task.FromResult(_castRepository.GetById(id));
    }

    public async Task<IEnumerable<Cast>> GetAllCastsAsync()
    {
        return await Task.FromResult(_castRepository.GetAll());
    }
}
