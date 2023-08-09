using Dapper;
using System.Data;

namespace FinancialApplication.Utility.Persistences.SqlServer
{
    public abstract class DapperRepository
    {
        public abstract IDbConnection Connection();

        public async Task<T> FindFirstOrDefaultAsync<T>(string query, object parameters = null)
        {
            using IDbConnection conn = Connection();
            conn.Open();
            T result = await conn.QueryFirstOrDefaultAsync<T>(query, parameters);
            return result;
        }

        public async Task<IEnumerable<T>> FindAsync<T>(string query, object parameters = null)
        {
            using IDbConnection conn = Connection();
            conn.Open();
            IEnumerable<T> result = await conn.QueryAsync<T>(query, parameters);
            return result;
        }

        public async Task ExecuteAsync(string query, object parameters = null, CommandType commandType = CommandType.Text)
        {
            using IDbConnection conn = Connection();
            conn.Open();
            await conn.ExecuteAsync(query, parameters, commandType: commandType);
        }

        public async Task<T> QuerySingleAsync<T>(string query, object parameters = null, CommandType commandType = CommandType.Text)
        {
            using IDbConnection conn = Connection();
            conn.Open();
            return await conn.QuerySingleAsync<T>(query, parameters, commandType: commandType);
        }
    }
}
