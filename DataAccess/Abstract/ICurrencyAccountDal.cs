using Core.Data.Abstract;
using Domain.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Abstract
{
    public interface ICurrencyAccountDal:IEntityRepository<CurrencyAccount>
    {
    }
}
