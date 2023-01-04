using Business.Abstract;
using Domain.Concrete;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Web.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MailParameterController : ControllerBase
    {
        private readonly IMailParameterService _mailParameterService;

        public MailParameterController(IMailParameterService mailParameterService)
        {
            _mailParameterService = mailParameterService;
        }

        [HttpPost]
        public async Task<IActionResult> AddorUpdateMailParameters(MailParameter mailParameter)
        {
            await _mailParameterService.AddOrUpdate(mailParameter);

            return Ok();
        }

        [HttpGet]
        public async Task<IActionResult> GetMailParameters()
        {

            return Ok(await _mailParameterService.GetAll());
        }



    }
}
