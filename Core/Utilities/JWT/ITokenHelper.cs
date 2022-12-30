using Core.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Utilities.JWT
{
    public interface ITokenHelper
    {
        /// <summary>
        /// User ,claimleri ve de company id yi alarak bu bilgiler dahilinde bir AccessToken oluşturacak
        /// </summary>
        /// <param name="user"></param>
        /// <param name="operationClaims"></param>
        /// <param name="companyId"></param>
        /// <returns></returns>
        AccessToken CreateToken(User user, List<OperationClaim> operationClaims, int companyId);
    }
}
