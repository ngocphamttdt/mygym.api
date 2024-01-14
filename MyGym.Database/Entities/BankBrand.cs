namespace MyGym.Database.Entities
{
    public  class BankBrand: BaseEntity
    {
        public string  Name { get; set; }
        public string Code { get; set; }

        public virtual ICollection<BankAccount> BankAccounts { get; set; }
    }
}
