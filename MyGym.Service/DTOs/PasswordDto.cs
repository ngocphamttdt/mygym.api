namespace MyGym.Service.DTOs
{
    public class PasswordDto
    {
        public byte[]? passwordHash { get; set; }
        public byte[]? passwordSalt { get; set; }
    }
}
