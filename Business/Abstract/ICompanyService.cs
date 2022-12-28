using Domain.Concrete;

namespace Business.Abstract
{
    public interface ICompanyService
    {
        Task AddAsync(Company company);
        Task UpdateAsync(Company company);
        Task DeleteAsync(Company company);
        Task<List<Company>> GetAllAsync();
        Task<Company> GetByIdAsync(int id);

    }
}
