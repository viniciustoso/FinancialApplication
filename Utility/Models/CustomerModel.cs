using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace FinancialApplication.Utility.Models
{
    public class CustomerModel
    {
        [JsonIgnore]
        public int Id { get; set; }

        [Required]
        public string CpfCnpj { get; set; }

        [Required]
        public string Name { get; set; }

        public int ContractNumber { get; set; }

        [Required]
        public string City { get; set; }

        [Required]
        public string State { get; set; }

        public decimal GrossIncome { get; set; }
    }
}
