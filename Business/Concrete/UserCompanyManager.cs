using Business.Abstract;
using Core.Utilities.Result.Abstract;
using Core.Utilities.Result.ComplexTypes;
using Core.Utilities.Result.Concrete;
using DataAccess.Abstract;
using Domain.Concrete;

namespace Business.Concrete
{
    public class UserCompanyManager : IUserCompanyService
    {
        private readonly IUserCompanyDal _userCompanyDal;

        public UserCompanyManager(IUserCompanyDal userCompanyDal)
        {
            _userCompanyDal = userCompanyDal;
        }

        public async Task<IResult> AddAsync(int userId, int companyId)
        {
            await _userCompanyDal.AddAsync(new UserCompany
            {
                AddedAt = DateTime.Now,
                CompanyId = companyId,
                IsActive= true,
                UserId=userId
                
            });
            await _userCompanyDal.SaveChangesAsync();
            return new Result(ResultStatus.Success);
        }
    }
}
