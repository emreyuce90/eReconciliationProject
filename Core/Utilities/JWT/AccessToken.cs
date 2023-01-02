using Microsoft.EntityFrameworkCore.Metadata;
using System.Security.AccessControl;

namespace Core.Utilities.JWT
{
    /// <summary>
    /// AccessToken sınıfımız ile üretilen tokenımızı ve ne kadar süre ayakta kalacağını belirleyeceğiz.Ayrıca CompanyId yi de token içerisinde gönderiyor olacağız
    /// </summary>
    public class AccessToken
    {
        public string Token { get; set; }
        public DateTime ExpirationDate { get; set; }
        public int CompanyId { get; set; }
    }
}
