using ApplicationCore.Entities;

namespace ApplicationCore.Repositories;

public interface IUserRepository: IRepository<User>
{
    User? GetByEmail(string email);
}