using Domain.Concrete;

namespace Business.Abstract
{
    public interface IMailParameterService
    {
        Task AddOrUpdate(MailParameter mailParameter);
        Task<MailParameter> Get(int id);
        Task<List<MailParameter>> GetAll();
    }
}
