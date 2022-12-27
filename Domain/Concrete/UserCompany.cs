using Core.Entities;

namespace Domain.Concrete
{
    public class UserCompany:IEntity
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int CompanyId { get; set; }
        public bool IsActive { get; set; }
        public DateTime AddedAt { get; set; }

    }
}
