namespace Core.Utilities.JWT
{
    /// <summary>
    /// Token 'da olması gereken özellikleri sınıf içerisinde topladık
    /// </summary>
    public class TokenOptions
    {
        public string Audience { get; set; }
        public string Issuer { get; set; }
        public int AccessTokenExpiration { get; set; }
        public string SecurityKey { get; set; }
    }
}
