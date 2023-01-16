using Core.Utilities.Result.Abstract;
using Domain.Concrete;

namespace Business.Abstract
{
    public interface IAccountReconciliationService
    {
        Task<IResult> AddAsync(AccountReconciliation accountReconciliation);

        Task<IResult> UpdateAsync(AccountReconciliation account);

        Task<IDataResult<List<AccountReconciliation>>> GetAllAsync();

        Task<IDataResult<AccountReconciliation>> GetAsync(int id);
        /// <summary>
        /// content içerisindeki excel dosyasını okur ve eğer validayona takılmaz ise içeriğini veritabanına kaydeder
        /// </summary>
        /// <param name="path">Excel dosyasının pathini alır</param>
        /// <param name="companyId">int tipinde companyId alır</param>
        /// <returns>Geriye başarı durumunu döner Success veya Failed ve eğer failed ise mesaj döner</returns>
        Task<IResult> AddToExcel(string path,int companyId);

    }
}
