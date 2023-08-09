using FinancialApplication.Payments.Models;
using FinancialApplication.Utility.Models;

namespace FinancialApplication.Payments.Persistences.SqlServer
{
    public interface IPaymentsRepository
    {
        Task<PaymentsModel> Find(int id);
        Task Save(PaymentsModel payment);
        Task Update(int idKey, PaymentsModel payment);
        Task Delete(int id);
        Task<decimal> GetPendingAmount(int contractNumber);
    }
}
