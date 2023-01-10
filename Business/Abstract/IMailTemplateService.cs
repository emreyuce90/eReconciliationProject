using Core.Utilities.Result.Abstract;
using Domain.Concrete;

namespace Business.Abstract
{
    public interface IMailTemplateService
    {
        Task<IResult> AddMailTemplate(MailTemplate mailTemplate);
        Task<IDataResult<MailTemplate>> GetMailTemplateById(int companyId,string name);
    }
}
