using Core.Data.Concrete;
using DataAccess.Abstract;
using DataAccess.Concrete.EntityFramework.Context;
using Domain.Concrete;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Concrete.EntityFramework.Repositories
{
    public class EfMailTemplateRepository : EfEntityRepository<MailTemplate>, IMailTemplateDal
    {
        private readonly eReconciliationDb _context;

        public EfMailTemplateRepository(eReconciliationDb context) : base(context)
        {
            _context = context;
        }
    }
}
