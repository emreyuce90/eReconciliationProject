

using Core.Entities;

namespace Domain.Concrete
{
    public class AccountReconciliationDetail:IEntity
    {
        public int Id { get; set; }
        public int AccountReconciliationsId { get; set; }
        public int CurrencyId { get; set; }
        public DateTime Date { get; set; }
        public decimal CurrencyDebit { get; set; }
        public decimal CurrencyCredit { get; set; }
        public string Description { get; set; }

    }
}
