using Business.Abstract;
using Business.CrossCuttingConcerns.ValidationRules;
using Core.CrossCuttingCoıncerns.Validation;
using Core.Entities.Concrete;
using Core.Utilities.Hashing;
using Core.Utilities.JWT;
using Core.Utilities.Result.Abstract;
using Core.Utilities.Result.ComplexTypes;
using Core.Utilities.Result.Concrete;
using Domain.Concrete;
using Domain.Concrete.Dtos;
using FluentValidation;

namespace Business.Concrete
{
    public class AuthManager : IAuthService
    {
        private readonly IMailTemplateService _mailTemplateService;
        private readonly IMailParameterService _mailParameterService;
        private readonly IMailSendService _mailSendService;
        private readonly IUserService _userService;
        private readonly ITokenHelper _tokenHelper;
        private readonly ICompanyService _companyService;
        private readonly IUserCompanyService _userCompanyService;
        public AuthManager(IUserService userService, ITokenHelper tokenHelper, ICompanyService companyService, IUserCompanyService userCompanyService, IMailParameterService mailParameterService, IMailSendService mailSendService, IMailTemplateService mailTemplateService)
        {
            _userService = userService;
            _tokenHelper = tokenHelper;
            _companyService = companyService;
            _userCompanyService = userCompanyService;
            _mailParameterService = mailParameterService;
            _mailSendService = mailSendService;
            _mailTemplateService = mailTemplateService;
        }

        public async Task<IResult> CheckCompanyExist(Company company)
        {
            var cResult = await _companyService.IsCompanyExists(company);
            if (cResult.ResultStatus == ResultStatus.Success)
            {
                return new Result(ResultStatus.Success);
            }
            return new Result(ResultStatus.Failed, "Firma ile ilgili bir hata meydana geldi");

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

        public async Task<IDataResult<UserCompany>> GetUsersCompanyByUserIdAsync(int userId)
        {
            return new DataResult<UserCompany>(await _userService.GetUserCompanyByUserIdAsync(userId),ResultStatus.Success);
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

        public async Task<IDataResult<UserCompanyDto>> Register(UserRegisterDto userRegisterDto, Company company)
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

            //validate et                   //IValidator tipinde nesne +obje
            ValidationHelper.ValidateObject(new UserValidatior(), user);

            //UserValidatior userValidator = new UserValidatior();
            //var validationResult =userValidator.Validate(user);
            //if (!validationResult.IsValid)
            //{
            //    throw new ValidationException(validationResult.Errors);
            //}


            await _userService.AddAsync(user);

            //company için validasyon yaz
            ValidationHelper.ValidateObject(new CompanyValidator(),company);
            //CompanyValidator companyValidator = new CompanyValidator();
            //var valResult = companyValidator.Validate(company);
            //if (!valResult.IsValid)
            //{
            //    throw new ValidationException(valResult.Errors);
            //}
            //companyyi kaydet
            await _companyService.AddAsync(company);
            //usercompanyyi kaydet
            await _userCompanyService.AddAsync(user.Id, company.Id);
            UserCompanyDto userCompanyDto = new UserCompanyDto()
            {
                AddedAt = user.AddedAt,
                CompanyId = company.Id,
                EMail = user.EMail,
                IsActive = true,
                MailConfirm = user.MailConfirm,
                MailConfirmDate = user.MailConfirmDate,
                MailConfirmValue = user.MailConfirmValue,
                Name = user.Name,
                PasswordHash = user.PasswordHash,
                Id = user.Id,
                PasswordSalt = user.PasswordSalt
            };
            await SendEmailAsync(user);
               
            return new DataResult<UserCompanyDto>(userCompanyDto, ResultStatus.Success);
        }

        public async Task<IResult> SendConfirmEmailAsync(User user)
        {
            //eğer kullanıcı maili zaten onaylandıysa
            if(user.MailConfirm == true)
            {
                return new Result(ResultStatus.Failed, "Bu mail adresi zaten onaylanmış");
            }

            if(user.MailConfirmDate.ToShortDateString() == DateTime.Now.ToShortDateString()) 
            {
                if (user.MailConfirmDate.Hour == DateTime.Now.Hour && user.MailConfirmDate.AddMinutes(5).Minute <= DateTime.Now.Minute)
                {
                    //mail gönder
                    await SendEmailAsync(user);
                    return new Result(ResultStatus.Success);

                }
                else
                {
                    return new Result(ResultStatus.Failed, "Lütfen 5dk sonra yeniden deneyiniz");

                }
            }

            return new Result(ResultStatus.Success);
        }

        public async Task<IDataResult<string>> SendEmailAsync(User user)
        {
            //html kodları
            string link = $"https://localhost:7031/api/auth/confirmMail/{user.MailConfirmValue}";
            string linkDescription = "Kaydı onaylamak için tıklayınız";
            string titleMessage = "Size gelen maili en geç 48 saat içerisinde onaylamalısınız";
            string message = "Hesabınız oluşturulmuştur.Lütfen email adresinize gelen aktivasyon linkine tıklayıp hesabınızı aktif ediniz";
            string title = "Mail Onayı";

            //mail template i manipüle etme
            var templateResult = await _mailTemplateService.GetMailTemplateById(2, "MailOnay");
            var mailData = templateResult.Data.Value;
            mailData = mailData.Replace("{{title}}", title);
            mailData = mailData.Replace("{{message}}", message);
            mailData = mailData.Replace("{{titleMessage}}", titleMessage);
            mailData = mailData.Replace("{{link}}", link);
            mailData = mailData.Replace("{{linkDescription}}", linkDescription);

            //mail sendDto nesnesini doldurma 

            var mailParameters = await _mailParameterService.Get(3);
            MailSendDto msDto = new()
            {
                ToEmail = user.EMail,
                Body = mailData,
                Subject = "Hesap Aktivasyonu",
                MailParameter = mailParameters
            };
            try
            {
                //mail gönderimi
                _mailSendService.SendMailAsync(msDto);
                user.MailConfirmDate=DateTime.Now;
                await _userService.UpdateUser(user);
                return new DataResult<string>(null, ResultStatus.Success, "Mail gönderimi başarılı");
            }
            catch (Exception ex)
            {

                return new DataResult<string>(null, ResultStatus.Failed, $"Mail gönderimi başarısız. Oluşan hatalar: {ex.Message}");


            }
        }


    }
}
