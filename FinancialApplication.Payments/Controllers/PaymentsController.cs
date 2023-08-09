using FinancialApplication.Payments.Persistences.SqlServer;
using FinancialApplication.Payments.Services;
using FinancialApplication.Utility.Models;
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
    public class PaymentsController : Controller
    {
        readonly PaymentsService PaymentService;

        public PaymentsController(
            IPaymentsRepository paymentRepository,
            IConfiguration configuration
        )
        {
            PaymentService = new PaymentsService(paymentRepository, configuration);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<PaymentsModel>> Get(int id)
        {
            try
            {
                return Ok(await PaymentService.Find(id));
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpPost]
        public async Task<ActionResult<string>> Post(PaymentsModel payment)
        {
            try
            {
                await PaymentService.Create(payment);
                return StatusCode(StatusCodes.Status201Created, "Payment registered successfully.");
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<string>> Put(int id, PaymentsModel payment)
        {
            try
            {
                await PaymentService.Update(id, payment);
                return Ok("Payment changed successfully.");
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<string>> Delete(int id)
        {
            try
            {
                await PaymentService.Delete(id);
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
    }
}
