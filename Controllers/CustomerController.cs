using Microsoft.AspNetCore.Mvc;
using System.Diagnostics.Metrics;
using WebApplication1.Models;
using WebApplication1.Service;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebApplication1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly ICustomerService _customerService;
        public CustomerController(ICustomerService customerService)
        {
            _customerService = customerService;
        }
        // GET: api/<CustomerController>
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            try
            {
                var customers = await _customerService.GetAllCustomersAsync();

                if (customers == null || !customers.Any())
                {
                    return NotFound("No customers found.");
                }

                return Ok(customers);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while processing your request.");
            }
        }

        [HttpGet("{page}")]
        public async Task<IActionResult> GetPage(string page, int pageSize = 10)
        {
            try
            {
                int pageNumber = int.Parse(page);
                var customers = await _customerService.GetPageCustomer(pageNumber, pageSize);

                if (customers == null || !customers.Any())
                {
                    return NotFound("No customers found.");
                }

                return Ok(customers);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while processing your request.");
            }
        }

        [HttpGet("count")]
        public async Task<IActionResult> GetCount()
        {
            try
            {
                int count = await _customerService.GetCount();
                return Ok(count);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while processing your request.");
            }
        }

        // GET api/<CustomerController>/5
        //[HttpGet("{id}")]
        //public async Task<IActionResult> Get(string? id, string? name, string? country)
        //{
        //    var customers = await _customerService.GetAllCustomersAsync();

        //    if (id != null)
        //    {
        //        customers = customers.Where(c => c.CustomerId == id);
        //    }
        //    if (name != null)
        //    {
        //        customers = customers.Where(c => c.CompanyName == name);
        //    }
        //    if (country != null)
        //    {
        //        customers = customers.Where(c => c.Country == country);
        //    }

        //    if (!customers.Any())
        //    {
        //        return NotFound();
        //    }

        //    return Ok(customers);
        //}

        // POST api/<CustomerController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }
    }
}
