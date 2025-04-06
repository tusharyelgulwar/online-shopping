using ApplicationCore.Entities;
using ApplicationCore.Models;

public interface IAccountService
{
    Task<User?> AuthenticateAsync(string email, string password);
    Task<User> RegisterUserAsync(RegisterModel model);
    Task<User?> GetUserByEmailAsync(string email);
    Task<string?> GetUserRoleAsync(int userId);
    Task<bool> IsUserAdminAsync(int userId);
}