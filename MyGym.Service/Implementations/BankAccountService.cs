using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;
using MyGym.Database;
using MyGym.Database.Entities;
using MyGym.Service.Contracts;
using MyGym.Service.DTOs;

namespace MyGym.Service.Implementations
{
    public class BankAccountService : IBankAccountService
    {
        private MyGymContext _context;
        public BankAccountService(MyGymContext context) {
            _context = context;
        }

        public async Task BulkCreateBankAccountAsync()
        {
            IList<BankAccount> bankAccounts = new List<BankAccount>();
            for (int i = 0; i < 8; i++)
            {
                var bankAccount = new BankAccount
                {
                    AccountNumber = Guid.NewGuid().ToString(),
                    BrandId = 2,
                    UserId = 4,
                    IsDefault = false
                };
                bankAccounts.Add(bankAccount);
            }
            await _context.BankAccounts.AddRangeAsync(bankAccounts);
            //await _context.SaveChangesAsync();
        }
        public void CreateBankAccount(BankAccountDto bankAccount)
        {
            BankAccount bankAcc = new BankAccount
            {
                AccountNumber = bankAccount.AccountNumber,
                BrandId = bankAccount.BrandId,
                UserId = bankAccount.UserId,
                IsDefault = bankAccount.IsDefault
            };
            _context.BankAccounts.Add(bankAcc);
            _context.SaveChanges();
        }

        public async Task<List<BankAccountDto>> GetAllBankAcocuntsByBrandIdAsync(int bankBrandId)
        {
            var bankBrand = await _context.BankBrands.Where(x=>x.Id == bankBrandId).Include(x=>x.BankAccounts).FirstOrDefaultAsync();
            var bankAccounts = new List<BankAccountDto>();
            if (bankBrand != null)
            {
                var bankAccountEntities = bankBrand.BankAccounts;
                foreach (var entity in bankAccountEntities)
                {
                    bankAccounts.Add(new BankAccountDto
                    {
                        AccountNumber = entity.AccountNumber,
                        BrandId = entity.BrandId,
                        UserId = entity.UserId,
                        IsDefault = entity.IsDefault
                    });
                }

            }
            return bankAccounts;
        }

        public BankAccount GetBankAccount(int accountId)
        {
            
                // Retrieve the BankAccount from the database
                var bankAccount = _context.BankAccounts.Find(accountId);

                // At this point, BankBrand property will be null
                // because lazy loading hasn't occurred yet

                return bankAccount;
            
        }

        public async Task<List<BankAccountDto>> GetAllBankAcocuntsAsync()
        {
            try
            {

                var bankAccount = GetBankAccount(1);
                var bankBrand = bankAccount.BankBrand;
                var test = "12";
            }
            catch (Exception ex)
            {

                throw ex;
            }
           


            //var bankBrands = _context.BankBrands.ToList();
            //var bankBrandsInclude = await _context.BankBrands.Include(x => x.BankAccounts).ToListAsync();

            //var bankAccs = await _context.BankAccounts.Include(x=>x.BankBrand).Where(x=>x.BankBrand.Code == "tpb")
            //   .ToListAsync();
            //var bankAccounts = new List<BankAccountDto>();
            #region memory
            //var bankQuery = _context.BankBrands.AsQueryable();
            //var queryRes = bankQuery.Where(x => x.Code.ToLower() == "tpb");

            //var queryEnumerable = _context.BankBrands.Where(x => x.Code.ToLower() == "tpb").AsEnumerable();
            //var test = queryEnumerable.FirstOrDefault();

            //var bankQ = _context.BankAccounts.AsQueryable();
            //bankQ = bankQ.Where(x => x.BrandId == 1);
            //var bankList = bankQ.ToList();


            //var bankE = _context.BankAccounts.AsEnumerable();
            //bankE = bankE.Where(x => x.BrandId == 1);

            //var bankList1 = bankE.ToList();

            //var bankList2 = bankE.ToList();
            //var bankBrand = _context.BankBrands.ToList();
            //var bankAccount = _context.BankAccounts.ToList();

            

            //var query = (from brand in bankBrand
            //            join account in bankAccount on brand.Id equals account.BrandId
            //            select new BankLinqDto {
            //                Id = brand.Id, 
            //                Name = brand.Name, 
            //                BankAccounts = bankAccount.Where(x=>x.Id == account.Id )
            //                .Select(x=> new BankAccountDto { AccountNumber = x.AccountNumber, BrandId = x.BrandId})
            //                .ToList()
            //            }
            //            ).ToList();

            //var firstQuery = bankQuery.Where(x => x.Code.ToLower() == "tpb").AsQueryable().FirstOrDefault();
            //var first = bankQuery.Where(x => x.Code.ToLower() == "tpb").FirstOrDefault();
            //var list = _context.BankBrands.ToList();
            //if (bankBrand != null)
            //{
            //    var bankAccountEntities = bankBrand.BankAccounts;
            //    foreach (var entity in bankAccountEntities)
            //    {
            //        bankAccounts.Add(new BankAccountDto
            //        {
            //            AccountNumber = entity.AccountNumber,
            //            BrandId = entity.BrandId,
            //            UserId = entity.UserId,
            //            IsDefault = entity.IsDefault
            //        });
            //    }

            //}
            #endregion
            //return bankAccounts;
            //IEnumerable<BankAccount> bankAcc_En = _context.BankAccounts;
            //ICollection<BankAccount> bankAcc_Co = _context.BankAccounts.ToList();

            //var firstEn = bankAcc_En.FirstOrDefault();
            //var firstEn1 = bankAcc_En.FirstOrDefault(x=> x.Id > 10);
            //var firstCount = bankAcc_En.Count();

            //var firstCo = bankAcc_Co.FirstOrDefault();
            //var firstCo1 = bankAcc_Co.FirstOrDefault(x => x.Id > 10);
            //var firstCountCo = bankAcc_Co.Count();
            return null;
        }

        public Task<string> UpdateBankAccountAsync(int bankId)
        {
            throw new NotImplementedException();
        }
    }
}
