using Business.Abstract;
using Core.Utilities.Result.Abstract;
using Core.Utilities.Result.ComplexTypes;
using Core.Utilities.Result.Concrete;
using Domain.Concrete;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Reflection.Metadata.Ecma335;

namespace Web.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountReconciliationDetailController : ControllerBase
    {
        private readonly IAccountReconciliationDetailService _accountReconciliationDetailService;

        public AccountReconciliationDetailController(IAccountReconciliationDetailService accountReconciliationDetailService)
        {
            _accountReconciliationDetailService = accountReconciliationDetailService;
        }

        [HttpGet("[action]")]
        public async Task<IActionResult> GetAllAsync()
        {
            var result = await _accountReconciliationDetailService.GetAccountReconciliationDetailAsync();
            if (result.ResultStatus == ResultStatus.Success)
            {

                return Ok(result.Data);
            }
            return BadRequest(result.Message);
        }
        [HttpPost("[action]")]
        public async Task<IActionResult> Add(AccountReconciliationDetail accountReconciliationDetail)
        {
            var result = await _accountReconciliationDetailService.AddAsync(accountReconciliationDetail);
            if (result.ResultStatus == ResultStatus.Success)
            {
                return Ok(result.Message);
            }
            return BadRequest(result.Message);
        }

        [HttpPut]
        public async Task<IActionResult> Update(AccountReconciliationDetail accountReconciliationDetail)
        {
            var result =await _accountReconciliationDetailService.UpdateAsync(accountReconciliationDetail);
            if (result.ResultStatus == ResultStatus.Success)
                return Ok(result.Message);
            return BadRequest(result.Message);

        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _accountReconciliationDetailService.DeleteAsync(id);
            if (result.ResultStatus == ResultStatus.Success)
                return Ok(result.Message);
            return BadRequest(result.Message);  
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var result =await _accountReconciliationDetailService.GetAccountReconciliationDetailById(id);
            if (result.ResultStatus == ResultStatus.Success)
                return Ok(result.Data);
            return BadRequest(result.Message);
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> AddToExcel(int accounReconId,IFormFile file)
        {
            if(file.Length >0)
            {
                //yeni isim oluştur
                var fileName = $"{Guid.NewGuid()}.xlsx";
                //dosya yolu
                var path = $"{Directory.GetCurrentDirectory()}/Content/{fileName}";
                //stream oluştur
                using (var stream = System.IO.File.Create(path))
                {
                    //copy to metoduyla dosyayı kopyala
                    await file.CopyToAsync(stream);
                    //streami flush et
                    await stream.FlushAsync();

                }
                //dosyayı oku ve veritabanına kaydet
                var result =await _accountReconciliationDetailService.AddToExcel(path,accounReconId);
                if(result.ResultStatus == ResultStatus.Success)
                {
                    return Ok(result.Message);
                }
                return BadRequest(result.Message);
            }
            return BadRequest("Lütfen geçerli bir excel dosyası giriniz");

        }
    }
}
