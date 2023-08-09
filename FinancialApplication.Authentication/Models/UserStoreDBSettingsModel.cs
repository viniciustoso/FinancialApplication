namespace FinancialApplication.Authentication.Models
{
    public class UserStoreDBSettingsModel
    {
        public string ConnectionString { get; set; }
        public string DatabaseName { get; set; }
        public string UsersCollectionName { get; set; }
    }
}
