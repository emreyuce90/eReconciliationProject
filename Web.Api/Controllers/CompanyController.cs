using Business.Abstract;
using Core.Utilities.Result.ComplexTypes;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

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
            if(result.ResultStatus == ResultStatus.Success)
                return Ok(result);
            return BadRequest(new
            {
                ErrorMessage = result.Exception.Message,
                ResultStatus = result.ResultStatus
            });
        }
    }
}
