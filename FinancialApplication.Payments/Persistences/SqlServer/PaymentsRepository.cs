using FinancialApplication.Payments.Models;
using FinancialApplication.Utility.Models;
using FinancialApplication.Utility.Persistences.SqlServer;
using System.Text;

namespace FinancialApplication.Payments.Persistences.SqlServer
{
    public class PaymentsRepository : SqlServerRepository, IPaymentsRepository
    {
        public PaymentsRepository(IConfiguration configuration)
            : base(configuration.GetConnectionString("SqlConnection"))
        {
        }

        public async Task<PaymentsModel> Find(int id)
        {
            const string query =
                @"SELECT *
                    FROM FinancialApplication.dbo.Payments
                   WHERE Id = @id";

            return await FindFirstOrDefaultAsync<PaymentsModel>(query, new { id });
        }

        public async Task Save(PaymentsModel payment)
        {
            const string query =
                @"INSERT
                    INTO FinancialApplication.dbo.Payments(ContractNumber, Installment, Value, Status)
                  VALUES (@ContractNumber, @Installment, @Value, @Status)";

            await ExecuteAsync(query, payment);
        }

        public async Task Update(int idKey, PaymentsModel payment)
        {
            const string query =
                @"UPDATE FinancialApplication.dbo.Payments
                     SET ContractNumber = @ContractNumber
                       , Installment = @Installment
                       , Value = @Value
                       , Status = @Status
                   WHERE Id = @Id";

            await ExecuteAsync(query, new
            {
                idKey, payment.ContractNumber, payment.Installment, payment.Value, payment.Status
            });
        }

        public async Task Delete(int id)
        {
            const string query =
                @"DELETE
                    FROM FinancialApplication.dbo.Payments
                   WHERE Id = @id";

            await ExecuteAsync(query, new { id });
        }

        public async Task<decimal> GetPendingAmount(int contractNumber)
        {
            const string query =
                @"SELECT SUM(Value)
                    FROM FinancialApplication.dbo.Payments
                   WHERE ContractNumber = @contractNumber
                     AND Status != 1";

            return await FindFirstOrDefaultAsync<decimal>(query, new { contractNumber });
        }
    }
}
