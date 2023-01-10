using Business.Abstract;
using Core.Utilities.Result.Abstract;
using Core.Utilities.Result.ComplexTypes;
using Core.Utilities.Result.Concrete;
using DataAccess.Abstract;
using Domain.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Concrete
{
    public class MailTemplateManager : IMailTemplateService
    {
        private readonly IMailTemplateDal _mailTemplateDal;

        public MailTemplateManager(IMailTemplateDal mailTemplateDal)
        {
            _mailTemplateDal = mailTemplateDal;
        }

        public async Task<IResult> AddMailTemplate(MailTemplate mailTemplate)
        {
            await _mailTemplateDal.AddAsync(mailTemplate);
            await _mailTemplateDal.SaveChangesAsync();
            return new Result(ResultStatus.Success);
        }

        public async Task<IDataResult<MailTemplate>> GetMailTemplateById(int companyId, string name)
        {
           var template = await _mailTemplateDal.GetSingle(t=>t.CompanyId == companyId && t.Type == name);
            if(template != null)
            {
                return new DataResult<MailTemplate>(template, ResultStatus.Success);

            }
            return new DataResult<MailTemplate>(null, ResultStatus.Failed);

        }
    }
}
