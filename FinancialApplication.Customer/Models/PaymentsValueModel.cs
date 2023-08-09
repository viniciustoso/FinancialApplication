using FinancialApplication.Utility.Models;
using System.Text.Json.Serialization;

namespace FinancialApplication.Customer.Models
{
    public class PaymentsValueModel
    {
        [JsonIgnore]
        public int ContractNumber { get; set; }
        public decimal Value { get; set; }
        public PaymentStatus Status { get; set; }
    }
}
