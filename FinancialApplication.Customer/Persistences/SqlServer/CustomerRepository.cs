using FinancialApplication.Customer.Models;
using FinancialApplication.Utility.Models;
using FinancialApplication.Utility.Persistences.SqlServer;

namespace FinancialApplication.Customer.Persistences.SqlServer
{
    public class CustomerRepository : SqlServerRepository, ICustomerRepository
    {
        public CustomerRepository(IConfiguration configuration)
            : base(configuration.GetConnectionString("SqlConnection"))
        {
        }

        public async Task<CustomerModel> Find(int id)
        {
            const string query =
                @"SELECT *
                    FROM FinancialApplication.dbo.Customer
                   WHERE Id = @id";

            return await FindFirstOrDefaultAsync<CustomerModel>(query, new { id });
        }

        public async Task<CustomerModel> FindByContractNumber(int contractNumber)
        {
            const string query =
                @"SELECT *
                    FROM FinancialApplication.dbo.Customer
                   WHERE ContractNumber = @contractNumber";

            return await FindFirstOrDefaultAsync<CustomerModel>(query, new { contractNumber });
        }

        public async Task Save(CustomerModel customer)
        {
            const string query =
                @"INSERT
                    INTO FinancialApplication.dbo.Customer(CpfCnpj, Name, ContractNumber, City, State, GrossIncome)
                  VALUES (@CpfCnpj, @Name, @ContractNumber, @City, @State, @GrossIncome)";

            await ExecuteAsync(query, customer);
        }

        public async Task Update(int idKey, CustomerModel customer)
        {
            const string query =
                @"UPDATE FinancialApplication.dbo.Customer
                     SET CpfCnpj = @CpfCnpj
                       , Name = @Name
                       , City = @City
                       , State = @State
                       , GrossIncome = @GrossIncome
                   WHERE Id = @idKey";

            await ExecuteAsync(query, new
            {
                idKey, customer.CpfCnpj, customer.Name, customer.ContractNumber, customer.City, customer.State, customer.GrossIncome
            });
        }

        public async Task Delete(int id)
        {
            const string query =
                @"DELETE
                    FROM FinancialApplication.dbo.Customer
                   WHERE Id = @id";

            await ExecuteAsync(query, new { id });
        }
    }
}
