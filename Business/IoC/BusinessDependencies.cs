using DataAccess.Abstract;
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
            services.AddScoped<IAccountReconciliationDal, EfAccountReconcilliationRepository>();
        }
    }
}
