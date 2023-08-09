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
    public class TotalPaymentsQtyController : Controller
    {
        readonly BIService BIService;

        public TotalPaymentsQtyController(IBIRepository biRepository)
        {
            BIService = new BIService(biRepository);
        }

        [HttpGet]
        public async Task<ActionResult<int>> Get()
        {
            try
            {
                return Ok(await BIService.GetPaymentsQty());
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
    }
}