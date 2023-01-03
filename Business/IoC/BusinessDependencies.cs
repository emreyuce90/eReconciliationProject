using Business.Abstract;
using Business.Concrete;
using Core.Utilities.JWT;
using DataAccess.Abstract;
using DataAccess.Concrete.EntityFramework.Context;
using DataAccess.Concrete.EntityFramework.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.IoC
{
    public static class BusinessDependencies
    {
        public static void AddBLLDependencies(this IServiceCollection services)
        {
            services.AddDbContext<eReconciliationDb>(opt=>opt.UseSqlServer("server=localhost;database=eMutabakat;integrated security=true;"));
            //DAL
            services.AddScoped<IAccountReconciliationDal, EfAccountReconciliationRepository>();
            services.AddScoped<IAccountReconciliationDetailDal, EfAccountReconciliationDetailRepository>();
            services.AddScoped<IBaBsReconciliationDal, EfBaBsReconciliationRepository>();
            services.AddScoped<IBaBsReconciliationDetailDal, EfBaBsReconciliationDetailRepository>();
            services.AddScoped<ICompanyDal, EfCompanyRepository>();
            services.AddScoped<ICurrencyAccountDal, EfCurrencyAccountRepository>();
            services.AddScoped<ICurrencyDal, EfCurrencyRepository>();
            services.AddScoped<IMailParameterDal, EfMailParameterRepository>();
            services.AddScoped<IOperationClaimDal, EfOperationClaimRepository>();
            services.AddScoped<IUserCompanyDal, EfUserCompanyRepository>();
            services.AddScoped<IUserDal, EfUserRepository>();
            services.AddScoped<IUserOperationClaimDal, EfUserOperationClaimRepository>();

            //BLL
            services.AddScoped<ICompanyService, CompanyManager>();
            services.AddScoped<IUserService,UserManager>();
            services.AddScoped<IAuthService, AuthManager>();
            services.AddScoped<ITokenHelper, JwtHelper>();
            services.AddScoped<IUserCompanyService, UserCompanyManager>();
        }
    }
}
