using Microsoft.AspNetCore.Mvc;
using MyGym.Service.Contracts;
using MyGym.Service.DTOs;

namespace MyGym.Controllers
{
    [Route("api/bank")]
    [ApiController]
    public class BankBrandController : ControllerBase
    {
        private IBankBrandService _bankBrandService;
        public BankBrandController(IBankBrandService bankBrandService)
        {
            _bankBrandService = bankBrandService;
        }

        [HttpGet]
        public async Task<IEnumerable<BankBrandDto>> Get(int bankBrandId)
        {
            return null;
        }

        [HttpPost]
        public async Task Post([FromBody] BankBrandDto bank)
        {
            _bankBrandService.CreateBankBrand(bank);
        }

        [HttpPut]
        public async Task<string> Put([FromBody] int bankId)
        {
            var updatedName = await _bankBrandService.UpdateBankBrandAsync(bankId);
            return updatedName;
        }
    }
}
