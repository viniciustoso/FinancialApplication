using System.ComponentModel;

namespace FinancialApplication.Utility.Models
{
    public enum PaymentStatus
    {
        [Description("Pago")]
        Paid = 1,
        [Description("Atrasado")]
        Late = 2,
        [Description("A Vencer")]
        Due = 3
    }
}
