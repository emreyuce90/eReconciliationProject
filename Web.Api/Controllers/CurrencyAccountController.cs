using Business.Abstract;
using Core.Utilities.Result.ComplexTypes;
using Domain.Concrete;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Web.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CurrencyAccountController : ControllerBase
    {
        private readonly ICurrencyAccountService _currencyAccountService;

        public CurrencyAccountController(ICurrencyAccountService currencyAccountService)
        {
            _currencyAccountService = currencyAccountService;
        }
        //list
        [HttpGet("GetAll")]
        public async Task<IActionResult> Get()
        {
            var result = await _currencyAccountService.GetAllAsync();
            if (result.ResultStatus == ResultStatus.Success)
                return Ok(result.Data);
            return BadRequest(result.Message);
        }
        //entity
        [HttpGet("GetSingle/{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var ca = await _currencyAccountService.GetSingle(id);
            if (ca.ResultStatus == ResultStatus.Success)
                return Ok(ca.Data);
            return BadRequest(ca.Message);
        }
        //add
        [HttpPost("AddCurrencyAccount")]
        public async Task<IActionResult> Post(CurrencyAccount currencyAccount)
        {
            var result = await _currencyAccountService.AddAsync(currencyAccount);
            if (result.ResultStatus == ResultStatus.Success)
                return Ok(result.Message);
            return BadRequest(result.Message);
        }

        //update
        [HttpPost("UpdateCurrencyAccount")]
        public async Task<IActionResult> Update(CurrencyAccount currencyAccount)
        {
            var res = await _currencyAccountService.UpdateAsync(currencyAccount);
            if (res.ResultStatus == ResultStatus.Success)
                return Ok(res.Message);
            return BadRequest(res.Message);
        }
        //delete
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _currencyAccountService.DeleteAsync(id);
            if (result.ResultStatus == ResultStatus.Success)
                return Ok(result.Message);
            return BadRequest(result.Message);
        }
    }
}
