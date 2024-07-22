using MyGym.Service.DTOs;

namespace MyGym.Service.Contracts
{
    public interface  IBankBrandService
    {
        void CreateBankBrand(BankBrandDto bankBrand);
        Task<List<BankBrandDto>> GetAllBankBrandsAsync();
        Task<string> UpdateBankBrandAsync(int bankId);
    }
}
