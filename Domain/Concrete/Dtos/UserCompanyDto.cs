using Core.Entities.Concrete;

namespace Domain.Concrete.Dtos
{
    public class UserCompanyDto:User
    {
        public int CompanyId { get; set; }
    }
}
