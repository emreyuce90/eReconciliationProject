using Business.Abstract;
using Core.Utilities.Result.ComplexTypes;
using Domain.Concrete;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Web.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountReconciliationController : ControllerBase
    {
        private readonly IAccountReconciliationService _accountReconciliationService;

        public AccountReconciliationController(IAccountReconciliationService accountReconciliationService)
        {
            _accountReconciliationService = accountReconciliationService;
        }

        [HttpGet("[action]")]
        public async Task<IActionResult> GetAll()
        {
            var datas = await _accountReconciliationService.GetAllAsync();
            if (datas.ResultStatus == ResultStatus.Success)
                return Ok(datas.Data);
            return BadRequest($"Result:{datas.ResultStatus} Message : {datas.Message}");
        }

        [HttpGet("[action]/{Id}")]
        public async Task<IActionResult> GetById([FromRoute] int Id)
        {
            var result = await _accountReconciliationService.GetAsync(Id);
            if (result.ResultStatus == ResultStatus.Success)
                return Ok(result.Data);
            return BadRequest(result.Message);
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> AddReconciliation(AccountReconciliation accountReconciliation)
        {
            var result = await _accountReconciliationService.AddAsync(accountReconciliation);
            if (result.ResultStatus == ResultStatus.Success)
                return Ok();
            return BadRequest(result.Message);
        }

        [HttpPut("[action]")]
        public async Task<IActionResult> UpdateReconciliation(AccountReconciliation accountReconciliation)
        {
            var result = await _accountReconciliationService.UpdateAsync(accountReconciliation);
            if (result.ResultStatus == ResultStatus.Success)
                return Ok();
            return BadRequest(result.Message);
        }
    }
}
