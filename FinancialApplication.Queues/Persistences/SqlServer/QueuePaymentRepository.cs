using FinancialApplication.Utility.Models;
using FinancialApplication.Utility.Persistences.SqlServer;

namespace FinancialApplication.Queues.Persistences.SqlServer
{
    public class QueuePaymentRepository : SqlServerRepository, IQueuePaymentRepository
    {
        public QueuePaymentRepository(IConfiguration configuration)
            : base(configuration.GetConnectionString("SqlConnection"))
        {
        }

        public async Task Save(PaymentsModel payment)
        {
            const string query =
                @"INSERT
                    INTO FinancialApplication.dbo.Payments(ContractNumber, Installment, Value, Status)
                  VALUES (@ContractNumber, @Installment, @Value, @Status)";

            await ExecuteAsync(query, payment);
        }
    }
}
