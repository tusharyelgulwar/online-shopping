using ApplicationCore.Entities;
using ApplicationCore.Models;
using ApplicationCore.Repositories;
using Infrastructure.Data;
using Infrastructure.Helpers;
using Microsoft.EntityFrameworkCore;


namespace Infrastructure.Services
{
    public class AccountService : IAccountService
    {
        private readonly MovieDbContext _context;
        private readonly IUserRepository _userRepository;

        public AccountService(MovieDbContext context, IUserRepository userRepository)
        {
            _context = context;
            _userRepository = userRepository;
        }

        public async Task<User?> AuthenticateAsync(string email, string password)
        {
            var user = await _context.Users
                .Include(u => u.UserRoles)
                .ThenInclude(ur => ur.Role)
                .FirstOrDefaultAsync(u => u.Email == email);

            if (user == null) return null;

            var hashedInput = PasswordHelper.HashPassword(password, user.Salt);
            return hashedInput == user.HashedPassword ? user : null;
        }

        public async Task<User> RegisterUserAsync(RegisterModel model)
        {
            var existing = _userRepository.GetByEmail(model.Email);
            if (existing != null)
                throw new Exception("User already exists");

            var salt = PasswordHelper.GenerateSalt();
            var hashedPassword = PasswordHelper.HashPassword(model.Password, salt);

            var user = new User
            {
                FirstName = model.FirstName,
                LastName = model.LastName,
                Email = model.Email,
                DateOfBirth = model.DateOfBirth,
                Salt = salt,
                HashedPassword = hashedPassword
            };

            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            // Assign "User" role by default
            var defaultRole = await _context.Roles.FirstOrDefaultAsync(r => r.Name == "User");
            if (defaultRole != null)
            {
                var userRole = new UserRole
                {
                    UserId = user.Id,
                    RoleId = defaultRole.Id
                };

                _context.UserRoles.Add(userRole);
                await _context.SaveChangesAsync();
            }

            return user;
        }

        public async Task<User?> GetUserByEmailAsync(string email)
        {
            return await _context.Users.FirstOrDefaultAsync(u => u.Email == email);
        }

        public async Task<string?> GetUserRoleAsync(int userId)
        {
            var user = await _context.Users
                .Include(u => u.UserRoles)
                .ThenInclude(ur => ur.Role)
                .FirstOrDefaultAsync(u => u.Id == userId);

            return user?.UserRoles.FirstOrDefault()?.Role?.Name;
        }

        public async Task<bool> IsUserAdminAsync(int userId)
        {
            var user = await _context.Users
                .Include(u => u.UserRoles)
                .ThenInclude(ur => ur.Role)
                .FirstOrDefaultAsync(u => u.Id == userId);

            return user?.UserRoles.Any(ur => ur.Role.Name == "Admin") ?? false;
        }
    }
}
