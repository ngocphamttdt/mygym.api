namespace MyGym.Database.Entities
{
    public  class BankAccount : BaseEntity
    {
        public string AccountNumber { get; set; }
        public int BrandId { get; set; }
        public int UserId { get; set;}
        public bool IsDefault { get; set; }

        public virtual BankBrand BankBrand { get; set; }
        public virtual User User { get; set; }
    }
}
