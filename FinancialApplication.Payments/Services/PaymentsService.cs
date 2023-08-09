using FinancialApplication.Payments.Models;
using FinancialApplication.Payments.Persistences.SqlServer;
using FinancialApplication.Utility.Exceptions;
using FinancialApplication.Utility.Models;
using FinancialApplication.Utility.Utilities;
using Newtonsoft.Json;

namespace FinancialApplication.Payments.Services
{
    public class PaymentsService
    {
        readonly IPaymentsRepository PaymentRepository;
        readonly IConfiguration Configuration;
        HttpClientUtility HttpClientUtility = new HttpClientUtility(null);

        public PaymentsService(
            IPaymentsRepository paymentRepository,
            IConfiguration configuration
        )
        {
            PaymentRepository = paymentRepository;
            Configuration = configuration;
        }

        public async Task<PaymentsModel> Find(int id)
        {
            return await PaymentRepository.Find(id);
        }

        public async Task<decimal> GetPendingAmount(int contractNumber)
        {
            return await PaymentRepository.GetPendingAmount(contractNumber);
        }

        public async Task Create(PaymentsModel payment)
        {
            await Validations(payment);

            JwtTokenModel token = await HttpClientUtility.PostAsync<JwtTokenModel>(
                $"{Configuration["BaseURL"]}Authentication/v1.0/Authenticate/",
                new UserModel
                {
                    User = Configuration["User"],
                    Password = Configuration["Password"]
                }
            );

            string paymentString = JsonConvert.SerializeObject(payment);

            await HttpClientUtility.PostAsync(
                $"{Configuration["BaseURL"]}Queues/v1.0/Publisher",
                new MessageInputModel
                {
                    QueueName = Configuration["CreatePaymentQueueName"],
                    Content = paymentString
                },
                $"Bearer {token.AccessToken}"
            );
        }

        public async Task Update(int id, PaymentsModel payment)
        {
            await PaymentRepository.Update(id, payment);
        }

        public async Task Delete(int id)
        {
            await PaymentRepository.Delete(id);
        }

        private async Task Validations(PaymentsModel payment)
        {
            JwtTokenModel token = await HttpClientUtility.PostAsync<JwtTokenModel>(
                $"{Configuration["BaseURL"]}Authentication/v1.0/Authenticate/",
                new UserModel {
                    User = Configuration["User"],
                    Password = Configuration["Password"]
                }
            );

            CustomerGrossIncomeModel customerGrossIncome = await HttpClientUtility.GetAsync<CustomerGrossIncomeModel>(
                $"{Configuration["BaseURL"]}Customer/v1.0/CustomerContract/{payment.ContractNumber}",
                null,
                $"Bearer {token.AccessToken}"
            );

            await ValidatePendingAmount(payment, customerGrossIncome);
        }

        private async Task ValidatePendingAmount(PaymentsModel payment, CustomerGrossIncomeModel customerGrossIncome)
        {
            if (payment.Status == PaymentStatus.Paid)
                return;

            decimal pendingAmount = await GetPendingAmount(payment.ContractNumber);

            if (pendingAmount + payment.Value > customerGrossIncome.GrossIncome)
                throw new ValidatorException("It is not possible to make a new payment, the sum of pending exceeds the customer's gross income.");
        }
    }
}
