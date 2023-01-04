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
        public async Task<IActionResult> Register(UserCompanyRegisterDto userCompanyRegisterDto)
        {

            //check user
            var checkResult = await _authService.CheckUserExist(userCompanyRegisterDto.UserRegisterDto.EMail);
            //chekck company
            var checkCompany = await _authService.CheckCompanyExist(userCompanyRegisterDto.Company);
            if (checkResult.ResultStatus == ResultStatus.Success && checkCompany.ResultStatus == ResultStatus.Success)
            {
                //hem userı hem de companyi authservice e gönderdik
                var registerResult = await _authService.Register(new UserRegisterDto()
                {
                    EMail = userCompanyRegisterDto.UserRegisterDto.EMail,
                    Password = userCompanyRegisterDto.UserRegisterDto.Password,
                    UserName = userCompanyRegisterDto.UserRegisterDto.UserName,
                }, userCompanyRegisterDto.Company);

                if (registerResult.ResultStatus == ResultStatus.Success)
                {
                    var tokenResult = await _authService.CreateToken(registerResult.Data, userCompanyRegisterDto.Company.Id);
                    return Ok(tokenResult);
                }
                else
                {
                    return BadRequest(registerResult.Message);

                }
            }
            return BadRequest($"Kullanıcı :{checkResult.Message}\n Şirket:{checkCompany.Message}");


        }
        [HttpPost("login")]
        public async Task<IActionResult> Login(UserLoginDto userLoginDto)
        {
            var loginResult = await _authService.Login(userLoginDto);
            if (loginResult.ResultStatus == ResultStatus.Success)
            {
                var tokenResult = await _authService.CreateToken(loginResult.Data, 0);
                if (tokenResult.ResultStatus == ResultStatus.Success)
                {
                    return Ok(tokenResult);
                }
                return BadRequest(tokenResult.Message);
            }
            return BadRequest(loginResult.Message);
        }
    }
}
