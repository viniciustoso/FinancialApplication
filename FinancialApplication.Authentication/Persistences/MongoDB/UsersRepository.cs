using FinancialApplication.Authentication.Models;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace FinancialApplication.Authentication.Persistences.MongoDB
{
    public class UsersRepository : IUsersRepository
    {
        private readonly IMongoCollection<UsersDBModel> UserCollection;

        public UsersRepository(
            IOptions<UserStoreDBSettingsModel> userStoreDBSettings)
        {
            var mongoClient = new MongoClient(
                userStoreDBSettings.Value.ConnectionString);

            var mongoDatabase = mongoClient.GetDatabase(
                userStoreDBSettings.Value.DatabaseName);

            UserCollection = mongoDatabase.GetCollection<UsersDBModel>(
                userStoreDBSettings.Value.UsersCollectionName
            );
        }

        public async Task<UsersDBModel> GetUserIdAsync(string userId) =>
            await UserCollection.Find(x => x.UserId == userId).FirstOrDefaultAsync();
    }
}
