using ApplicationCore.Entities;

namespace ApplicationCore.Contracts.Services;

public interface ICastService
{
    Task<Cast?> GetCastByIdAsync(int id);
    Task<IEnumerable<Cast>> GetAllCastsAsync();
}