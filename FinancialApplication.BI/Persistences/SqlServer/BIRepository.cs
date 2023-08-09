using FinancialApplication.BI.Models;
using FinancialApplication.Utility.Models;
using FinancialApplication.Utility.Persistences.SqlServer;
using System.Linq;
using System.Text;

namespace FinancialApplication.BI.Persistences.SqlServer
{
    public class BIRepository : SqlServerRepository, IBIRepository
    {
        public BIRepository(IConfiguration configuration)
            : base(configuration.GetConnectionString("SqlConnection"))
        {
        }

        public async Task<int> GetPaymentsQty(PaymentStatus[] status = null)
        {
            string queryBase =
                @"SELECT COUNT(*)
                    FROM FinancialApplication.dbo.Payments
                   #STATUSFILTER"
            ;

            StringBuilder query = new StringBuilder(queryBase);
            query.Replace("#STATUSFILTER",
                status is not null ? "WHERE Status IN @status" : string.Empty);

            return await FindFirstOrDefaultAsync<int>(query.ToString(), new { status });
        }

        public async Task<IEnumerable<CustomersByStateStatusModel>> GetCustomersByStateAndPaymentStatus()
        {
            const string query =
                @"SELECT C.State
	                   , P.Status
	                   , COUNT(*) 'CustomersQty'
                    FROM Customer C
                    JOIN Payments P ON P.ContractNumber = C.ContractNumber
                   GROUP BY C.State, P.Status
                   ORDER BY C.State, P.Status";

            return await FindAsync<CustomersByStateStatusModel>(query);
        }

        public async Task<IEnumerable<PaymentsByStateModel>> GetPaymentsByState()
        {
            const string query =
                @"SELECT C.State
	                   , COUNT(*) 'PaymentsQty'
	                   , SUM(P.Value) 'PaymentsValue'
                    FROM Customer C
                    JOIN Payments P ON P.ContractNumber = C.ContractNumber
                   GROUP BY C.State
                   ORDER BY C.State";

            return await FindAsync<PaymentsByStateModel>(query);
        }

        public async Task<IEnumerable<AverageGrossIncomeByStateModel>> GetAverageGrossIncomeByState()
        {
            const string query =
                @"SELECT State
	                   , AVG(GrossIncome) 'AverageGrossIncome'
                    FROM Customer
                   GROUP BY State
                   ORDER BY State";

            return await FindAsync<AverageGrossIncomeByStateModel>(query);
        }
    }
}