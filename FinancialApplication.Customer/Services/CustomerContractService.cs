using FinancialApplication.Customer.Models;
using FinancialApplication.Customer.Persistences.SqlServer;
using FinancialApplication.Utility.Exceptions;
using FinancialApplication.Utility.Models;

namespace FinancialApplication.Customer.Services
{
    public class CustomerContractService
    {
        readonly ICustomerRepository CustomerRepository;

        public CustomerContractService(ICustomerRepository customerRepository)
        {
            CustomerRepository = customerRepository;
        }

        public async Task<CustomerModel> Find(int contractNumber)
        {
            CustomerModel customer = await CustomerRepository.FindByContractNumber(contractNumber);

            if (customer is null)
                throw new KeyNotFoundException("Customer not found.");

            return customer;
        }
    }
}