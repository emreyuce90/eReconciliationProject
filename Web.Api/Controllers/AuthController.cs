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
        [HttpPost("login")]
        public async Task<IActionResult> Login(UserLoginDto userLoginDto)
        {
            var loginResult=await _authService.Login(userLoginDto);
            if(loginResult.ResultStatus == ResultStatus.Success)
            {
                var tokenResult= await _authService.CreateToken(loginResult.Data, 0);
                if(tokenResult.ResultStatus== ResultStatus.Success)
                {
                    return Ok(tokenResult);
                }
                return BadRequest(tokenResult.Message);
            }
            return BadRequest(loginResult.Message);
        }
    }
}
