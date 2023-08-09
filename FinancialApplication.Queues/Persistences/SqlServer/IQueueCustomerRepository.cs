using FinancialApplication.Utility.Models;

namespace FinancialApplication.Queues.Persistences.SqlServer
{
    public interface IQueueCustomerRepository
    {
        Task Save(CustomerModel customer);
    }
}
