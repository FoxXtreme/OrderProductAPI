using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OrderProductAPI.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Storage;

namespace OrderProductAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly OrderProductAPIContext _context;

        public OrdersController(OrderProductAPIContext context)
        {
            _context = context;
        }

        // GET: api/orders - Tüm siparişleri listeleme
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Order>>> GetOrders()
        {
            var orders = await _context.Orders.Include(o => o.Product).ToListAsync();
            return Ok(orders);
        }

        // GET: api/orders/{id} - Belirli bir siparişi getirme
        [HttpGet("{id}")]
        public async Task<ActionResult<Order>> GetOrderById(int id)
        {
            var order = await _context.Orders.Include(o => o.Product).FirstOrDefaultAsync(o => o.Id == id);

            if (order == null)
            {
                return NotFound();
            }

            return Ok(order);
        }

        // POST: api/orders - Yeni bir sipariş ekleme
        [HttpPost]
        public async Task<ActionResult<Order>> CreateOrder([FromBody] Order order)
        {
            // Model validation kontrolü
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            // Ürün ID'si geçerli mi kontrol et
            var product = await _context.Products.FindAsync(order.ProductId);
            if (product == null)
            {
                return BadRequest("Geçersiz ürün ID'si.");
            }

            // Siparişin Product nesnesini ilişkilendir
            order.Product = product;

            // Tarihleri ayarla
            order.CreatedAt = DateTime.UtcNow;
            order.UpdatedAt = DateTime.UtcNow;
            order.OrderDate = DateTime.UtcNow;

            // Transaction kullanarak işlem güvenliğini artır
            using (IDbContextTransaction transaction = await _context.Database.BeginTransactionAsync())
            {
                try
                {
                    // Siparişi veritabanına ekle
                    _context.Orders.Add(order);
                    await _context.SaveChangesAsync();

                    // Commit işlemi
                    await transaction.CommitAsync();

                    // Siparişi döndür
                    return CreatedAtAction(nameof(GetOrderById), new { id = order.Id }, order);
                }
                catch (Exception)
                {
                    // Hata durumunda rollback yap
                    await transaction.RollbackAsync();
                    return StatusCode(500, "Bir hata oluştu. Sipariş işlemi tamamlanamadı.");
                }
            }
        }

        // GET: api/orders/total - Toplam sipariş tutarını hesaplayan bir endpoint
        [HttpGet("total")]
        public async Task<ActionResult<decimal>> GetTotalOrderAmount()
        {
            var totalAmount = await _context.Orders
                .Include(o => o.Product)
                .SumAsync(o => o.Quantity * o.Product.Price);

            return Ok(totalAmount);
        }
    }
}
