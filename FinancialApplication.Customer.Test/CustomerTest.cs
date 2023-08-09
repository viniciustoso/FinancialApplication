using FinancialApplication.Customer.Persistences.SqlServer;
using FinancialApplication.Customer.Services;
using FinancialApplication.Utility.Exceptions;
using FinancialApplication.Utility.Models;
using Microsoft.Extensions.Configuration;
using Moq;
using Xunit;

namespace FinancialApplication.Customer.Test
{
    public class CustomerTest
    {
        private readonly CustomerService CustomerService;

        public CustomerTest()
        {
            CustomerService = new CustomerService(
                new Mock<ICustomerRepository>().Object,
                new Mock<IConfiguration>().Object
            );
        }

        [Fact]
        public async Task CustomerNotFoundTest()
        {
            Func<Task> act = () => CustomerService.Find(0);
            var ex = await Assert.ThrowsAsync<KeyNotFoundException>(act);
        }

        [Fact]
        public async Task InvalidCpfCnpjTest()
        {
            CustomerModel customer = new CustomerModel
            {
                CpfCnpj = "123"
            };

            Func<Task> act = () => CustomerService.Create(customer);
            var ex = await Assert.ThrowsAsync<ValidatorException>(act);
        }
    }
}