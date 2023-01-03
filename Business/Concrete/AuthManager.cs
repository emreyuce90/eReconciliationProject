using Business.Abstract;
using Core.Entities.Concrete;
using Core.Utilities.Hashing;
using Core.Utilities.JWT;
using Core.Utilities.Result.Abstract;
using Core.Utilities.Result.ComplexTypes;
using Core.Utilities.Result.Concrete;
using Domain.Concrete;
using Domain.Concrete.Dtos;

namespace Business.Concrete
{
    public class AuthManager : IAuthService
    {
        private readonly IUserService _userService;
        private readonly ITokenHelper _tokenHelper;
        private readonly ICompanyService _companyService;
        private readonly IUserCompanyService _userCompanyService;
        public AuthManager(IUserService userService, ITokenHelper tokenHelper, ICompanyService companyService, IUserCompanyService userCompanyService)
        {
            _userService = userService;
            _tokenHelper = tokenHelper;
            _companyService = companyService;
            _userCompanyService = userCompanyService;
        }

        public async Task<IResult> CheckCompanyExist(Company company)
        {
            var cResult = await _companyService.IsCompanyExists(company);
            if (cResult.ResultStatus == ResultStatus.Success)
            {
                return new Result(ResultStatus.Success);
            }
            return new Result(ResultStatus.Failed,"Firma ile ilgili bir hata meydana geldi");

        }

        public async Task<IResult> CheckUserExist(string email)
        {
            User user = await _userService.GetUserByEMail(email);
            if (user != null)
            {
                return new Result(ResultStatus.Failed, "Sistemde zaten böyle bir kullanıcı kayıtlı");
            }
            return new Result(ResultStatus.Success);

        }

        public async Task<IDataResult<AccessToken>> CreateToken(User user, int companyId)
        {
            var claimList = await _userService.GetOperationClaims(companyId, user);
            var accessToken = _tokenHelper.CreateToken(user, claimList, companyId);
            return new DataResult<AccessToken>(accessToken, ResultStatus.Success);
        }

        public async Task<IDataResult<User>> Login(UserLoginDto userLoginDto)
        {
            //check user
            var user = await _userService.GetUserByEMail(userLoginDto.EMail);
            if (user == null)
            {
                return new DataResult<User>(null, ResultStatus.Failed, "Böyle bir kullanıcı bulunamadı");
            }
            //check password
            bool isTrue = HashingHelper.VerifyPasswordHash(userLoginDto.Password, user.PasswordHash, user.PasswordSalt);
            if (isTrue)
            {
                return new DataResult<User>(user, ResultStatus.Success);
            }

            return new DataResult<User>(null, ResultStatus.Failed, "Kullanıcı adı veya şifre hatalı");


        }

        public async Task<IDataResult<UserCompanyDto>> Register(UserRegisterDto userRegisterDto,Company company)
        {
            byte[] passwordHash, passwordSalt;
            HashingHelper.CreatePasswordHash(userRegisterDto.Password, out passwordHash, out passwordSalt);
            User user = new()
            {
                AddedAt = DateTime.Now,
                EMail = userRegisterDto.EMail,
                IsActive = true,
                MailConfirm = false,
                MailConfirmDate = DateTime.Now,
                MailConfirmValue = Guid.NewGuid().ToString(),
                Name = userRegisterDto.UserName,
                PasswordHash = passwordHash,
                PasswordSalt = passwordSalt
            };
            await _userService.AddAsync(user);
            //companyyi kaydet
            await _companyService.AddAsync(company);
            //usercompanyyi kaydet
            await _userCompanyService.AddAsync(user.Id, company.Id);
            UserCompanyDto userCompanyDto = new UserCompanyDto()
            {
                AddedAt=user.AddedAt,
                CompanyId=company.Id,
                EMail=user.EMail,
                IsActive= true,
                MailConfirm=user.MailConfirm,
                MailConfirmDate=user.MailConfirmDate,
                MailConfirmValue=user.MailConfirmValue,
                Name=user.Name,
                PasswordHash=user.PasswordHash,
                Id=user.Id,
                PasswordSalt=user.PasswordSalt
            };
            return new DataResult<UserCompanyDto>(userCompanyDto, ResultStatus.Success);
        }
    }
}
