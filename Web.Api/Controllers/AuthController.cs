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
        private readonly IUserService _userService;
        public AuthController(IAuthService authService, IUserService userService)
        {
            _authService = authService;
            _userService = userService;
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
            if(!loginResult.Data.IsActive)
                return BadRequest("Kullanıcı aktif değil");

            if (loginResult.ResultStatus == ResultStatus.Success)
            {
                //kullanıcıa ait companyId yi çekelim
                var uc = await _authService.GetUsersCompanyByUserIdAsync(loginResult.Data.Id);
                //companyId yi de token ın içerisine dolduralım
                var tokenResult = await _authService.CreateToken(loginResult.Data,uc.Data.CompanyId);
                if (tokenResult.ResultStatus == ResultStatus.Success)
                {
                    return Ok(tokenResult);
                }
                return BadRequest(tokenResult.Message);
            }
            return BadRequest(loginResult.Message);
        }

        [HttpGet("confirmMail/{confirmValue}")]
        public async Task<IActionResult> ConfirmMail([FromRoute]string confirmValue)
        {
            var user = await _userService.GetUserByConfirmValue(confirmValue);
            if (user.MailConfirmValue == confirmValue)
            {
                user.MailConfirmDate = DateTime.Now;
                user.MailConfirm = true;
                await _userService.UpdateUser(user);
                return Ok();

            }
            return BadRequest("Kullanıcı veya onay hatası");
        }

        [HttpGet("sendConfirmMail")]
        public async Task<IActionResult> ReConfirmByUser(int userId)
        {
            var user = await _userService.GetUserByUserId(userId);
            //email gönderme operasyonu
            var result = await _authService.SendConfirmEmailAsync(user);
            if(result.ResultStatus == ResultStatus.Success)
            {
                return Ok(result.Message);
            }
            return BadRequest(result.Message);
            
        }
    }
}
