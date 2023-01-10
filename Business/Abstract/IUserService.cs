using Core.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Abstract
{
    public interface IUserService
    {
        Task<List<OperationClaim>> GetOperationClaims(int companyId, User user);
        Task AddAsync(User user);
        Task<User> GetUserByEMail(string email);
        Task<User> GetUserByUserId(int id);
        Task<bool> UpdateUser(User user);
    }
}
