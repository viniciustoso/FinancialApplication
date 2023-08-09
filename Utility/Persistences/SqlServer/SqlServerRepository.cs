using System.Data;
using System.Data.SqlClient;

namespace FinancialApplication.Utility.Persistences.SqlServer
{
    public class SqlServerRepository : DapperRepository
    {
        readonly string ConnectionString;

        public SqlServerRepository(string connectionString)
        {
            ConnectionString = connectionString;
        }

        public override IDbConnection Connection()
        {
            return new SqlConnection(ConnectionString);
        }
    }
}
