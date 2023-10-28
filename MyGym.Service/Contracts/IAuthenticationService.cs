using MyGym.Service.DTOs;

namespace MyGym.Service.Contracts
{
    public interface IAuthenticationService
    {
        PasswordDto CreatePasswordHash(string password);
        bool VerifyPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt);

    }
}
