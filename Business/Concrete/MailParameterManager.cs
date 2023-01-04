using Business.Abstract;
using DataAccess.Abstract;
using Domain.Concrete;
using Microsoft.EntityFrameworkCore;

namespace Business.Concrete
{
    public class MailParameterManager : IMailParameterService
    {
        private readonly IMailParameterDal _mailParameterDal;

        public MailParameterManager(IMailParameterDal mailParameterDal)
        {
            _mailParameterDal = mailParameterDal;
        }

        public async Task AddOrUpdate(MailParameter mailParameter)
        {
            var mailP = await _mailParameterDal.GetSingle(mp => mp.CompanyId == mailParameter.CompanyId);
            if (mailP != null)
            {
                //güncelleme
                mailP.SSL = mailParameter.SSL;
                mailP.Port = mailParameter.Port;
                mailP.Password=mailParameter.Password;
                mailP.EMail = mailParameter.EMail;
                mailP.SMTP = mailParameter.SMTP;
                _mailParameterDal.Update(mailP);
                await _mailParameterDal.SaveChangesAsync();
            }
            else
            {
                //yeni mail
                await _mailParameterDal.AddAsync(mailParameter);
                await _mailParameterDal.SaveChangesAsync();
            }


        }

        public async Task<MailParameter> Get(int companyId)
        {
           var mp=await _mailParameterDal.GetSingle(m => m.CompanyId == companyId);
            return mp;
        }

        public async Task<List<MailParameter>> GetAll()
        {
            var mp=await _mailParameterDal.GetAll(false).ToListAsync();
            return mp;
        }
    }
}
