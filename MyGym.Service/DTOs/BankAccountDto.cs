using MyGym.Database.Entities;

namespace MyGym.Service.DTOs
{
    public  class BankAccountDto
    {
        public string AccountNumber { get; set; }
        public int BrandId { get; set; }
        public string BrandCode { get; set; }
        public int UserId { get; set; }
        public bool IsDefault { get; set; }

    }
}
