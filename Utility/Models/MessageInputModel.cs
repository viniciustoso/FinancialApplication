namespace FinancialApplication.Utility.Models
{
    public class MessageInputModel
    {
        public string QueueName { get; set; }
        public string Content { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
    }
}
