using FinancialApplication.BI.Models;
using FinancialApplication.BI.Persistences.SqlServer;
using FinancialApplication.BI.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FinancialApplication.BI.Controllers
{
    [Authorize]
    [ApiController]
    [Route("v{version:apiVersion}/[controller]")]
    [ApiVersion("1.0")]
    [Consumes("application/json")]
    [Produces("application/json")]
    public class PaymentsByStateController : Controller
    {
        readonly BIService BIService;

        public PaymentsByStateController(IBIRepository biRepository)
        {
            BIService = new BIService(biRepository);
        }

        [HttpGet]
        public async Task<ActionResult<IList<PaymentsByStateModel>>> Get()
        {
            try
            {
                return Ok(await BIService.GetPaymentsByState());
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
    }
}