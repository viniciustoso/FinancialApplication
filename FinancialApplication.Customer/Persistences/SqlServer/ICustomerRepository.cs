using FinancialApplication.Utility.Models;

namespace FinancialApplication.Customer.Persistences.SqlServer
{
    public interface ICustomerRepository
    {
        Task<CustomerModel> Find(int id);
        Task<CustomerModel> FindByContractNumber(int contractNumber);
        Task Save(CustomerModel customer);
        Task Update(int idKey, CustomerModel customer);
        Task Delete(int id);
    }
}
