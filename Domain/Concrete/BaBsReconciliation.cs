

using Core.Entities;

namespace Domain.Concrete
{
    public class BaBsReconciliation : IEntity
    {
        public int Id { get; set; }
        public int CompanyId { get; set; }
        public int CurrencyAccountId { get; set; }
        public string Type { get; set; }
        public int Month { get; set; }
        public int Year { get; set; }
        public int Quantity { get; set; }
        public decimal Total { get; set; }
        public bool IsEmailSend { get; set; }
        public DateTime? SendEmailDate { get; set; }
        public bool? IsEMailRead { get; set; }
        public DateTime? EmailReadDate { get; set; }
        public bool IsResultSucceded { get; set; }
        public DateTime? ResultDate { get; set; }
        public string? ResultNote { get; set; }

    }
}
