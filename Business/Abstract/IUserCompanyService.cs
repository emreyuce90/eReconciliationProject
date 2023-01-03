using Core.Utilities.Result.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Abstract
{
    public interface IUserCompanyService
    {
        Task<IResult> AddAsync(int userId, int companyId);
    }
}
