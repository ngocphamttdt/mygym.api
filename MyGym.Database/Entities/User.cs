namespace MyGym.Database.Entities
{
    public class User : BaseEntity
    {
        public string UserName { get; set; }
        public byte[]? PasswordHash { get; set; }
        public byte[]? PasswordSalt { get; set; }
        public string? RefreshToken { get; set; }
        public DateTime? TokenCreated { get; set; }
        public DateTime? TokenExpires { get; set; }
        public virtual ICollection<Product>? Products { get; set; }
        public virtual ICollection<BankAccount>? BankAccounts { get; set; }

    }
}
