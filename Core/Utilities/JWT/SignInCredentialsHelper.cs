using Microsoft.IdentityModel.Tokens;

namespace Core.Utilities.JWT
{
    public class SignInCredentialsHelper
    {
        /// <summary>
        /// Buraya oluşturduğumuz symmetric keyimizi vereceğiz ve bize bunu kriptolayıp geri döndürecek
        /// </summary>
        /// <param name="securityKey">SecuritKey tipinde bir security key alır</param>
        /// <returns>Geriye SigningCredentials döner</returns>
        public static SigningCredentials CreateSigningCredentials(SecurityKey securityKey)
        {
            return new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha512Signature);
        }
    }
}
