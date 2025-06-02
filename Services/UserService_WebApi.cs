using Microsoft.EntityFrameworkCore;
using WebApi.Models;

namespace WebApi.Services
{
    public class UserService
    {
        private readonly AppDbContext _context;
        private readonly AuthService _auth;

        public UserService(AppDbContext context, AuthService auth)
        {
            _context = context;
            _auth = auth;
        }

        public async Task<User?> RegisterAsync(string name, string email, string password, string role = "User")
        {
            var exists = await _context.Users.AnyAsync(u => u.Email == email);
            if (exists) return null;

            var user = new User
            {
                Name = name,
                Email = email,
                PasswordHash = _auth.HashPassword(password),
                Role = role
            };

            _context.Users.Add(user);
            await _context.SaveChangesAsync();
            return user;
        }

        public async Task<string?> LoginAsync(string email, string password)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == email);
            if (user == null) return null;

            if (!_auth.VerifyPassword(password, user.PasswordHash)) return null;

            return _auth.GenerateJwtToken(user);
        }
    }
}
