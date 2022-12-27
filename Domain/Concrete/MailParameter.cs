

using Core.Entities;

namespace Domain.Concrete
{
    public class MailParameter:IEntity
    {
        public int Id { get; set; }
        public int CompanyId { get; set; }
        public string EMail { get; set; }
        public string Password { get; set; }
        public string SMTP { get; set; }
        public int Port { get; set; }
        public bool SSL { get; set; }
    }
}
