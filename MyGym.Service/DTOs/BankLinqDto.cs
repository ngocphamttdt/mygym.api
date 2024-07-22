namespace MyGym.Service.DTOs
{
    public  class BankLinqDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<BankAccountDto> BankAccounts { get; set; }
    }
}
