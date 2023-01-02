using Core.Entities.Concrete;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace Core.Utilities.JWT
{
    public class JwtHelper : ITokenHelper
    {
        //appSettingsJson dosyasını okuyacağız ve buradaki değerleri kullanarak tokenımızı oluşturacağız
        public IConfiguration Configuration { get; }
        //appSettings.json daki verileri property olarak tokenOptions a set edip nesne olarak kullanacağız
        private TokenOptions _tokenOptions;
        //configden okuduğumuz değeri buraya atayacağız
        DateTime _accessTokenExpiration;
        public JwtHelper(IConfiguration configuration)
        {
            Configuration = configuration;
            _tokenOptions = Configuration.GetSection("TokenOptions").Get<TokenOptions>();
        }

        public AccessToken CreateToken(User user, List<OperationClaim> operationClaims, int companyId)
        {
            //token ın geçerlilik süresi
            _accessTokenExpiration = DateTime.Now.AddMinutes(_tokenOptions.AccessTokenExpiration);
            var securityKey = SecurityKeyHelper.CreateSecurityKey(_tokenOptions.SecurityKey);
            var signinCredentials = SignInCredentialsHelper.CreateSigningCredentials(securityKey);
            var jwt = CreateJwtSecurityToken(_tokenOptions, user, signinCredentials, operationClaims, companyId);
            var jwtSecurityTokenHandler = new JwtSecurityTokenHandler();
            var token = jwtSecurityTokenHandler.WriteToken(jwt);
            return new AccessToken { CompanyId= companyId ,ExpirationDate =_accessTokenExpiration,Token=token};
        }
        public JwtSecurityToken CreateJwtSecurityToken(TokenOptions tokenOptions, User user, SigningCredentials signingCredentials, List<OperationClaim> operationClaims, int companyId)
        {
            var jwt = new JwtSecurityToken(
                issuer: tokenOptions.Issuer,
                audience: tokenOptions.Audience,
                claims: SetClaims(user,operationClaims,companyId),
                notBefore: DateTime.Now, expires: _accessTokenExpiration, signingCredentials: signingCredentials);
               
            return jwt;
        }

        private IEnumerable<Claim> SetClaims(User user,List<OperationClaim> operationClaims,int companyId)
        {
            var claims = new List<Claim>();
            claims.AddNameIdentifier(user.Id.ToString());
            claims.AddEMail(user.EMail);
            claims.AddName($"{user.Name}");
            claims.AddRoles(operationClaims.Select(c => c.Name).ToArray());
            claims.AddCompany(companyId.ToString());
            return claims;
        }
    }
}
