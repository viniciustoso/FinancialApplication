using FinancialApplication.Utility.Models;
using System.Text.Json.Serialization;

namespace FinancialApplication.Utility.Models
{
    public class PaymentsModel
    {
        [JsonIgnore]
        public int Id { get; set; }
        public int ContractNumber { get; set; }
        public int Installment { get; set; }
        public decimal Value { get; set; }
        public PaymentStatus Status { get; set; }
    }
}
