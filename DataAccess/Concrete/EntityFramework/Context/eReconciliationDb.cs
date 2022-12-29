using Core.Entities.Concrete;
using Domain.Concrete;
using Microsoft.EntityFrameworkCore;
namespace DataAccess.Concrete.EntityFramework.Context
{
    public class eReconciliationDb:DbContext
    {
        public eReconciliationDb(DbContextOptions options) : base(options)
        {
        }

        public DbSet<AccountReconciliation> AccountReconciliations { get; set; }
        public DbSet<AccountReconciliationDetail> AccountReconciliationDetails { get; set; }
        public DbSet<BaBsReconciliation> BaBsReconciliations { get; set; }
        public DbSet<BaBsReconciliationDetail> BaBsReconciliationDetails { get; set; }
        public DbSet<Company> Companies { get; set; }
        public DbSet<Currency> Currencies { get; set; }
        public DbSet<CurrencyAccount> CurrencyAccounts { get; set; }
        public DbSet<MailParameter> MailParameters { get; set; }
        public DbSet<UserCompany> UserCompanies { get; set; }
        public DbSet<OperationClaim> OperationClaims { get; set; }
        public DbSet<UserOperationClaim> UserOperationClaims { get; set; }
        public DbSet<User> Users { get; set; }
    }
}
