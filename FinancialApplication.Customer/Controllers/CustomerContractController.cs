using FinancialApplication.Customer.Models;
using FinancialApplication.Customer.Persistences.SqlServer;
using FinancialApplication.Customer.Services;
using FinancialApplication.Utility.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FinancialApplication.Customer.Controllers
{
    [Authorize]
    [ApiController]
    [Route("v{version:apiVersion}/[controller]")]
    [ApiVersion("1.0")]
    [Consumes("application/json")]
    [Produces("application/json")]
    public class CustomerContractController : Controller
    {
        readonly CustomerContractService CustomerContractService;

        public CustomerContractController(ICustomerRepository customerRepository)
        {
            CustomerContractService = new CustomerContractService(customerRepository);
        }

        [HttpGet("{contractNumber}")]
        public async Task<ActionResult<CustomerModel>> Get(int contractNumber)
        {
            try
            {
                return Ok(await CustomerContractService.Find(contractNumber));
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
    }
}
