using FinancialApplication.Payments.Persistences.SqlServer;
using FinancialApplication.Payments.Services;
using FinancialApplication.Utility.Exceptions;
using FinancialApplication.Utility.Models;
using Microsoft.Extensions.Configuration;
using Moq;
using Xunit;

namespace PaymentsTest
{
    public class PaymentsTest
    {
        private readonly PaymentsService PaymentsService;

        public PaymentsTest()
        {
            PaymentsService = new PaymentsService(
                new Mock<IPaymentsRepository>().Object,
                new Mock<IConfiguration>().Object
            );
        }

        [Fact]
        public async Task PendingExceedsGrossIncomeTest()
        {
            PaymentsModel payment = new PaymentsModel
            {
                Value = 50,
                Status = PaymentStatus.Late
            };

            Func<Task> act = () => PaymentsService.Create(payment);
            var ex = await Assert.ThrowsAsync<ValidatorException>(act);
        }
    }
}