using FinancialApplication.Customer.Models;
using FinancialApplication.Customer.Persistences.SqlServer;
using FinancialApplication.Customer.Services;
using FinancialApplication.Utility.Exceptions;
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
    public class CustomerController : Controller
    {
        readonly CustomerService CustomerService;

        public CustomerController(
            ICustomerRepository customerRepository,
            IConfiguration configuration
        )
        {
            CustomerService = new CustomerService(customerRepository, configuration);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<CustomerModel>> Get(int id)
        {
            try
            {
                return Ok(await CustomerService.Find(id));
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

        [HttpPost]
        public async Task<ActionResult<string>> Post(CustomerModel customer)
        {
            try
            {
                await CustomerService.Create(customer);
                return StatusCode(StatusCodes.Status201Created, "Customer registered successfully.");
            }
            catch (ValidatorException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<string>> Put(int id, CustomerModel customer)
        {
            try
            {
                await CustomerService.Update(id, customer);
                return Ok("Customer changed successfully.");
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
                await CustomerService.Delete(id);
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
    }
}
