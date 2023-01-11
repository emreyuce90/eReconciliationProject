using Business.Abstract;
using Core.Utilities.Result.ComplexTypes;
using Domain.Concrete;
using Domain.Concrete.Dtos;
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

        [HttpGet("GetAllCompanies")]
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

        [HttpPost("AddCompany")]
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
        [HttpDelete("DeleteCompany/{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _companyService.DeleteAsync(id);
            if (result.ResultStatus == ResultStatus.Success)
                return Ok(result);
            return BadRequest(result);
        }

        [HttpPut("UpdateCompany")]
        public async Task<IActionResult> UpdateCompany(Company company)
        {
            var result = await _companyService.UpdateAsync(company);
            if (result.ResultStatus == ResultStatus.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpPost("AddCompanyRelationalWithUserId")]
        public async Task<IActionResult> AddUserCompany(UserCompanyAddDto userCompanyAddDto)
        {
            var result = await _companyService.AddCompanyRelationalUser(userCompanyAddDto);
            if (result.ResultStatus == ResultStatus.Success)
                return Ok();
            return BadRequest(result.Message);
        }
    }
}
