using FinancialApplication.BI.Persistences.SqlServer;
using FinancialApplication.BI.Services;
using FinancialApplication.Utility.Models;
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
    public class PendingPaymentsQtyController : Controller
    {
        readonly BIService BIService;

        public PendingPaymentsQtyController(IBIRepository biRepository)
        {
            BIService = new BIService(biRepository);
        }

        [HttpGet]
        public async Task<ActionResult<int>> Get()
        {
            try
            {
                return Ok(await BIService.GetPaymentsQty(new PaymentStatus[] { PaymentStatus.Late, PaymentStatus.Due }));
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
    }
}