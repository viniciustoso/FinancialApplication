using FinancialApplication.Queues.Models;
using FinancialApplication.Queues.Persistences.SqlServer;
using FinancialApplication.Utility.Models;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;

namespace FinancialApplication.Queues.Consumers
{
    public class CreateCustomerConsumer : BasicConsumer
    {
        readonly IQueueCustomerRepository QueueCustomerRepository;

        public CreateCustomerConsumer(
            IOptions<RabbitMqConfiguration> option,
            IQueueCustomerRepository queueCustomerRepository
        ) : base("CreateCustomer", option)
        {
            QueueCustomerRepository = queueCustomerRepository;
        }

        protected override async Task DoWork(MessageInputModel message)
        {
            CustomerModel customer = JsonConvert.DeserializeObject<CustomerModel>(message.Content);
            await QueueCustomerRepository.Save(customer);
        }
    }
}
