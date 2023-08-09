using FinancialApplication.BI.Models;
using FinancialApplication.Utility.Models;

namespace FinancialApplication.BI.Persistences.SqlServer
{
    public interface IBIRepository
    {
        Task<int> GetPaymentsQty(PaymentStatus[] status = null);
        Task<IEnumerable<CustomersByStateStatusModel>> GetCustomersByStateAndPaymentStatus();
        Task<IEnumerable<PaymentsByStateModel>> GetPaymentsByState();
        Task<IEnumerable<AverageGrossIncomeByStateModel>> GetAverageGrossIncomeByState();
    }
}
