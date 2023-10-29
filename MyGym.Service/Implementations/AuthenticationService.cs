using MyGym.Service.Contracts;
using MyGym.Service.DTOs;
using System.Security.Cryptography;

namespace MyGym.Service.Implementations
{
    public class AuthenticationService : IAuthenticationService
    {
        public AuthenticationService() { }

        public PasswordDto CreatePasswordHash(string password)
        {
            var hmac = new HMACSHA512();

            var passwordSalt = hmac.Key;
            var passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));

            return new PasswordDto
            {
                passwordHash = passwordHash,
                passwordSalt = passwordSalt
            };
        }

        public bool VerifyPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt)
        {
            var hmac = new HMACSHA512(passwordSalt);
            var computedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            return computedHash.SequenceEqual(passwordHash);
        }
    }
}
