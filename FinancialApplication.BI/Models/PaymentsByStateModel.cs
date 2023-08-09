namespace FinancialApplication.BI.Models
{
    public class PaymentsByStateModel
    {
        public string State { get; set; }
        public int PaymentsQty { get; set; }
        public decimal PaymentsValue { get; set; }
    }
}
