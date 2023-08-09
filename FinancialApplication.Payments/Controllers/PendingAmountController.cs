using FinancialApplication.Payments.Models;
using FinancialApplication.Payments.Persistences.SqlServer;
using FinancialApplication.Payments.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FinancialApplication.Payments.Controllers
{
    [Authorize]
    [ApiController]
    [Route("v{version:apiVersion}/[controller]")]
    [ApiVersion("1.0")]
    [Consumes("application/json")]
    [Produces("application/json")]
    public class PendingAmountController : Controller
    {
        readonly PaymentsService PaymentService;

        public PendingAmountController(
            IPaymentsRepository paymentRepository,
            IConfiguration configuration
        )
        {
            PaymentService = new PaymentsService(paymentRepository, configuration);
        }

        [HttpGet("{contractNumber}")]
        public async Task<ActionResult<decimal>> Get(int contractNumber)
        {
            try
            {
                return Ok(await PaymentService.GetPendingAmount(contractNumber));
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
    }
}