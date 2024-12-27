using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OrderProductAPI.Models;
using OrderProductAPI.Models.OrderProductAPICustomer.Models;
using OrderProductAPI.Services;


namespace OrderProductAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomersController : ControllerBase
    {
        private readonly OrderProductAPIContext _context;

        public CustomersController(OrderProductAPIContext context)
        {
            _context = context;
        }

        // POST: api/customers
        [HttpPost]
        public async Task<IActionResult> CreateCustomer([FromBody] Customer customer)
        {
            if (customer == null)
            {
                return BadRequest("Customer cannot be null.");
            }

            if (string.IsNullOrEmpty(customer.CustomerName) || string.IsNullOrEmpty(customer.CustomerEmail))
            {
                return BadRequest("Customer name and email are required.");
            }

            _context.Customers.Add(customer);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetCustomers), new { id = customer.Id }, customer);
        }

        // GET: api/customers
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Customer>>> GetCustomers()
        {
            return await _context.Customers.ToListAsync();
        }
    }
}
