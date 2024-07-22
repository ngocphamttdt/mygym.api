using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MyGym.Service.Contracts;
using MyGym.Service.DTOs;

namespace MyGym.Controllers
{
    [Route("api/bankaccount")]
    [ApiController]
    public class BankAccountController : ControllerBase
    {
        private IBankAccountService  _bankAccount; 
        private IGuidService _guidService1;
        private IGuidService _guidService2;
        public BankAccountController(IBankAccountService bankAccount, IGuidService guidService1, IGuidService guidService2)
        {
            _bankAccount = bankAccount;
            _guidService1 = guidService1;
            _guidService2 = guidService2;
        }

        //[HttpGet]
        //public async Task<IEnumerable<BankAccountDto>> Get()
        //{
        //    var data = await _bankAccount.GetAllBankAcocuntsAsync();
        //    return data;
        //}

        [HttpGet]
        public async Task<IEnumerable<BankAccountDto>> Get()
        {
            //var guid1 = _guidService1.GetGuid();
            //var guid2 = _guidService1.GetGuid();
            //var guid3 = _guidService2.GetGuid();

            var data = await _bankAccount.GetAllBankAcocuntsAsync();
            return null;
        }

        //[HttpGet]
        //public async Task<IEnumerable<BankAccountDto>> Get()
        //{
        //    var data = await _bankAccount.GetAllBankAcocuntsAsync();
        //    return data;
        //}

        //[HttpGet("{id:int}")]
        //[Route("brand")]
        //public async Task<IEnumerable<BankAccountDto>> GetByBrand(int id)
        //{
        //    var data = await _bankAccount.GetAllBankAcocuntsByBrandIdAsync(id);
        //    return data;
        //}


        [HttpPost]
        [Route("bulk-create")]
        public async Task Post()
        {
            await _bankAccount.BulkCreateBankAccountAsync();
        }

        [HttpPost]
        public async Task Post([FromBody] BankBrandDto bank)
        {
           
        }

        [HttpPut]
        public async Task<string> Put([FromBody] int bankId)
        {
           
            return null;
        }


    }
}
