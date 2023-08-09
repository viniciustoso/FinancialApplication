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
    public class CustomersByStateStatusController : Controller
    {
        readonly BIService BIService;

        public CustomersByStateStatusController(IBIRepository biRepository)
        {
            BIService = new BIService(biRepository);
        }

        [HttpGet]
        public async Task<ActionResult<IList<CustomersByStateStatusModel>>> Get()
        {
            try
            {
                return Ok(await BIService.GetCustomersByStateAndPaymentStatus());
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
    }
}