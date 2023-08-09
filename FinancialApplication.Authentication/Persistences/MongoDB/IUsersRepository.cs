using FinancialApplication.Authentication.Models;

namespace FinancialApplication.Authentication.Persistences.MongoDB
{
    public interface IUsersRepository
    {
        Task<UsersDBModel> GetUserIdAsync(string userId);
    }
}
