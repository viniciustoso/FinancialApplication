using FinancialApplication.Utility.Models;
using FinancialApplication.Utility.Persistences.SqlServer;

namespace FinancialApplication.Queues.Persistences.SqlServer
{
    public class QueueCustomerRepository : SqlServerRepository, IQueueCustomerRepository
    {
        public QueueCustomerRepository(IConfiguration configuration)
            : base(configuration.GetConnectionString("SqlConnection"))
        {
        }

        public async Task Save(CustomerModel customer)
        {
            const string query =
                @"INSERT
                    INTO FinancialApplication.dbo.Customer(CpfCnpj, Name, ContractNumber, City, State, GrossIncome)
                  VALUES (@CpfCnpj, @Name, @ContractNumber, @City, @State, @GrossIncome)";

            await ExecuteAsync(query, customer);
        }
    }
}
