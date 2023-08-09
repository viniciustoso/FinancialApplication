using FinancialApplication.BI.Models;
using FinancialApplication.BI.Persistences.SqlServer;
using FinancialApplication.Utility.Models;

namespace FinancialApplication.BI.Services
{
    public class BIService
    {
        readonly IBIRepository BIRepository;

        public BIService(IBIRepository biRepository)
        {
            BIRepository = biRepository;
        }

        public async Task<int> GetPaymentsQty(PaymentStatus[] status = null)
        {
            return await BIRepository.GetPaymentsQty(status);
        }

        public async Task<IList<CustomersByStateStatusModel>> GetCustomersByStateAndPaymentStatus()
        {
            IEnumerable<CustomersByStateStatusModel> reportList = await BIRepository.GetCustomersByStateAndPaymentStatus();
            return reportList.ToList();
        }

        public async Task<IList<PaymentsByStateModel>> GetPaymentsByState()
        {
            IEnumerable<PaymentsByStateModel> reportList = await BIRepository.GetPaymentsByState();
            return reportList.ToList();
        }

        public async Task<IList<AverageGrossIncomeByStateModel>> GetAverageGrossIncomeByStateModel()
        {
            IEnumerable<AverageGrossIncomeByStateModel> reportList = await BIRepository.GetAverageGrossIncomeByState();
            return reportList.ToList();
        }
    }
}