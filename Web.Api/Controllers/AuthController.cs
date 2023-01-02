using Business.Abstract;
using Core.Utilities.Result.ComplexTypes;
using Domain.Concrete.Dtos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Web.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(UserRegisterDto userRegisterDto)
        {

            //check user
            var checkResult = await _authService.CheckUserExist(userRegisterDto.EMail);
            if(checkResult.ResultStatus == ResultStatus.Success)
            {
                var registerResult = await _authService.Register(new UserRegisterDto()
                {
                    EMail = userRegisterDto.EMail,
                    Password = userRegisterDto.Password,
                    UserName = userRegisterDto.UserName,
                });
                if(registerResult.ResultStatus == ResultStatus.Success)
                {
                    return Ok(registerResult);
                }
                else
                {
                    return BadRequest(registerResult.Message);

                }
            }
            return BadRequest(checkResult.Message);
            

        }
    }
}
