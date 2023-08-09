using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace FinancialApplication.Authentication.Models
{
    public class UsersDBModel
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        public string UserId { get; set; }

        public string Password { get; set; }

        public string PasswordToken { get; set; }

        public int LifecycleMinutes { get; set; } = 30;

        public bool IsSuspended { get; set; } = false;
    }
}
