using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OrderProductAPI.Models;
using OrderProductAPI.Services;

namespace OrderProductAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class QueryController : ControllerBase
    {
        private readonly OrderProductAPIContext _context;

        public QueryController(OrderProductAPIContext context)
        {
            _context = context;
        }

        // GET: api/products/expensive
        [HttpGet("expensive")]
        public async Task<ActionResult<IEnumerable<Product>>> GetExpensiveProducts()
        {
            return await _context.Products.Where(p => p.Price > 500).ToListAsync();
        }

        // GET: api/products/most-ordered
        [HttpGet("most-ordered")]
        public async Task<ActionResult<Product>> GetMostOrderedProduct()
        {
            var mostOrderedProduct = await _context.Products
                .OrderByDescending(p => p.Orders.Count)
                .FirstOrDefaultAsync();

            if (mostOrderedProduct == null)
            {
                return NotFound();
            }

            return mostOrderedProduct;
        }

        // GET: api/products/total-stock
        [HttpGet("total-stock")]
        public async Task<ActionResult<int>> GetTotalStock()
        {
            var totalStock = await _context.Products.SumAsync(p => p.Stock);
            return Ok(totalStock);
        }

        // GET: api/orders/after-date
        [HttpGet("orders/after-date")]
        public async Task<ActionResult<IEnumerable<Order>>> GetOrdersAfterDate([FromQuery] DateTime date)
        {
            return await _context.Orders.Where(o => o.OrderDate > date).ToListAsync();
        }
    }
}