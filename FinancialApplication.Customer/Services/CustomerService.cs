using FinancialApplication.Customer.Persistences.SqlServer;
using FinancialApplication.Utility.Exceptions;
using FinancialApplication.Utility.Models;
using FinancialApplication.Utility.Utilities;
using FinancialApplication.Utility.Validations;
using Newtonsoft.Json;

namespace FinancialApplication.Customer.Services
{
    public class CustomerService
    {
        readonly ICustomerRepository CustomerRepository;
        readonly IConfiguration Configuration;
        HttpClientUtility HttpClientUtility = new HttpClientUtility(null);

        public CustomerService(
            ICustomerRepository customerRepository,
            IConfiguration configuration
        )
        {
            CustomerRepository = customerRepository;
            Configuration = configuration;
        }

        public async Task<CustomerModel> Find(int id)
        {
            CustomerModel customer = await CustomerRepository.Find(id);

            if (customer is null)
                throw new KeyNotFoundException("Customer not found.");

            return customer;
        }

        public async Task Create(CustomerModel customer)
        {
            if (!CpfCnpjValidation.IsValid(customer.CpfCnpj))
                throw new ValidatorException("Invalid CpfCnpj field.");

            JwtTokenModel token = await HttpClientUtility.PostAsync<JwtTokenModel>(
                $"{Configuration["BaseURL"]}Authentication/v1.0/Authenticate/",
                new UserModel
                {
                    User = Configuration["User"],
                    Password = Configuration["Password"]
                }
            );

            string customerString = JsonConvert.SerializeObject(customer);

            await HttpClientUtility.PostAsync(
                $"{Configuration["BaseURL"]}Queues/v1.0/Publisher",
                new MessageInputModel
                {
                    QueueName = Configuration["CreateCustomerQueueName"],
                    Content = customerString
                },
                $"Bearer {token.AccessToken}"
            );
        }

        public async Task Update(int id, CustomerModel customer)
        {
            await Validations(id, customer);
            await CustomerRepository.Update(id, customer);
        }

        public async Task Delete(int id)
        {
            await CustomerRepository.Delete(id);
        }

        public async Task Validations(int id, CustomerModel customer)
        {
            await Find(id);
            await ValidateOutstandingPayments(customer);
        }

        private async Task ValidateOutstandingPayments(CustomerModel customer)
        {
            JwtTokenModel token = await HttpClientUtility.PostAsync<JwtTokenModel>(
                $"{Configuration["BaseURL"]}Authentication/v1.0/Authenticate/",
                new UserModel
                {
                    User = Configuration["User"],
                    Password = Configuration["Password"]
                }
            );

            decimal pendingAmount = await HttpClientUtility.GetAsync<decimal>(
                $"{Configuration["BaseURL"]}Payments/v1.0/PendingAmount/{customer.ContractNumber}",
                null,
                $"Bearer {token.AccessToken}"
            );

            if (customer.GrossIncome < pendingAmount)
                throw new ValidatorException("Unable to update customer, the sum of pending exceeds the customer's gross income.");
        }
    }
}
