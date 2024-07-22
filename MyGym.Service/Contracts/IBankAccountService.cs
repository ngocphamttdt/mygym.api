using MyGym.Service.DTOs;

namespace MyGym.Service.Contracts
{
    public interface IBankAccountService
    {
        Task BulkCreateBankAccountAsync();
        void CreateBankAccount(BankAccountDto bankAccount);
        Task<List<BankAccountDto>> GetAllBankAcocuntsByBrandIdAsync(int bankBrandId);
        Task<List<BankAccountDto>> GetAllBankAcocuntsAsync();
        Task<string> UpdateBankAccountAsync(int bankAccountId);
    }
}
