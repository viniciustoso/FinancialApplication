using FinancialApplication.Queues.Models;
using FinancialApplication.Queues.Persistences.SqlServer;
using FinancialApplication.Utility.Models;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;

namespace FinancialApplication.Queues.Consumers
{
    public class CreatePaymentConsumer : BasicConsumer
    {
        readonly IQueuePaymentRepository QueuePaymentRepository;

        public CreatePaymentConsumer(
            IOptions<RabbitMqConfiguration> option,
            IQueuePaymentRepository queuePaymentRepository
        ) : base("CreatePayment", option)
        {
            QueuePaymentRepository = queuePaymentRepository;
        }

        protected override async Task DoWork(MessageInputModel message)
        {
            PaymentsModel payment = JsonConvert.DeserializeObject<PaymentsModel>(message.Content);
            await QueuePaymentRepository.Save(payment);
        }
    }
}
