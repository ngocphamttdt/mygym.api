using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using MyGym.Database;
using MyGym.Database.Entities;
using MyGym.Service.Contracts;
using MyGym.Service.DTOs;

namespace MyGym.Service.Implementations
{
    public class BankBrandService : IBankBrandService
    {
        private MyGymContext _context;
        public BankBrandService( MyGymContext context) { 
            _context = context;
        }
        public void CreateBankBrand(BankBrandDto bankBrand)
        {
           BankBrand bank = new BankBrand();
            bank.Name = bankBrand.Name;
            bank.Code = bankBrand.Code;
            _context.Attach(bank);
            _context.SaveChanges();
        }

        public async Task<List<BankBrandDto>> GetAllBankBrandsAsync()
        {
          var data =  await _context.BankBrands.AsTracking().Select(x=> new BankBrandDto
          {
              Id = x.Id,
              Code = x.Code,
              Name = x.Name,
          }).ToListAsync();   
        return data;
        }

        public async Task<string> UpdateBankBrandAsync(int bankId)
        {
            BankBrand bank = await _context.BankBrands.FindAsync(bankId);
            bank.Name = "Vietcombank12";
            bank.Code = "vcb12";
            var state = _context.Entry(bank).State;
            await _context.SaveChangesAsync();
            var state1 = _context.Entry(bank).State;
            return bank.Name;
        }
    }
}
