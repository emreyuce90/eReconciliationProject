using Business.Abstract;
using Core.Utilities.Result.ComplexTypes;
using Domain.Concrete;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Runtime.CompilerServices;

namespace Web.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CompanyController : ControllerBase
    {
        private readonly ICompanyService _companyService;

        public CompanyController(ICompanyService companyService)
        {
            _companyService = companyService;
        }

        [HttpGet]
        public async Task<IActionResult> GetCompanies()
        {
            var result = await _companyService.GetAllAsync();
            if (result.ResultStatus == ResultStatus.Success)
                return Ok(result);
            return BadRequest(new
            {
                ErrorMessage = result.Exception.Message,
                ResultStatus = result.ResultStatus
            });
        }

        [HttpPost]
        public async Task<IActionResult> AddCompany(Company company)
        {
            if (ModelState.IsValid)
            {
                var result = await _companyService.AddAsync(company);
                if (result.ResultStatus == ResultStatus.Success)
                {
                    return Ok(result);
                }
                else
                {
                    return BadRequest(result);
                }

            }
            return BadRequest("Validasyon Hatası");
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _companyService.DeleteAsync(id);
            if (result.ResultStatus == ResultStatus.Success)
                return Ok(result);
            return BadRequest(result);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateCompany(Company company)
        {
            var result =await _companyService.UpdateAsync(company);
            if(result.ResultStatus == ResultStatus.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }
    }
}
