using FinancialApplication.Utility.Models;

namespace FinancialApplication.Queues.Persistences.SqlServer
{
    public interface IQueuePaymentRepository
    {
        Task Save(PaymentsModel payment);
    }
}
