using Core.Utilities.Result.Abstract;
using Domain.Concrete;

namespace Business.Abstract
{
    public interface ICompanyService
    {
        Task<IResult> AddAsync(Company company);
        Task<IResult> UpdateAsync(Company company);
        Task<IResult>DeleteAsync(int id);
        Task<IDataResult<List<Company>>> GetAllAsync();
        Task<IDataResult<Company>> GetByIdAsync(int id);

    }
}
