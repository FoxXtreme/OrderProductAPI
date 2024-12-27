using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OrderProductAPI.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OrderProductAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly OrderProductAPIContext _context;

        public ProductsController(OrderProductAPIContext context)
        {
            _context = context;
        }

        // GET /api/products: Tüm ürünleri listeleme
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Product>>> GetProducts()
        {
            var products = await _context.Products.ToListAsync();
            return Ok(products);
        }

        // GET /api/products/{id}: Belirli bir ürünü getirme
        [HttpGet("{id}")]
        public async Task<ActionResult<Product>> GetProduct(int id)
        {
            var product = await _context.Products.FindAsync(id);

            if (product == null)
            {
                return NotFound("Ürün bulunamadı.");
            }

            return Ok(product);
        }

        // POST /api/products: Yeni bir ürün ekleme
        [HttpPost]
        public async Task<ActionResult<Product>> CreateProduct(Product product)
        {
            if (!ModelState.IsValid)
            {
                // Geçersiz model durumunda ModelState'i döndürmek
                return BadRequest(ModelState);
            }

            product.CreatedAt = DateTime.UtcNow;
            product.UpdatedAt = DateTime.UtcNow;

            _context.Products.Add(product);

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException ex)
            {
                return StatusCode(500, $"Bir hata oluştu: {ex.Message}");
            }

            return CreatedAtAction(nameof(GetProduct), new { id = product.Id }, product);
        }


        // PUT /api/products/{id}: Mevcut bir ürünü güncelleme
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateProduct(int id, Product product)
        {
            if (id != product.Id)
            {
                return BadRequest("Ürün ID'si uyuşmuyor.");
            }

            var existingProduct = await _context.Products.FindAsync(id);
            if (existingProduct == null)
            {
                return NotFound("Ürün bulunamadı.");
            }

            existingProduct.Name = product.Name;
            existingProduct.Price = product.Price;
            existingProduct.Stock = product.Stock;
            existingProduct.UpdatedAt = DateTime.UtcNow; // sadece güncelleme tarihi değişecek

            _context.Entry(existingProduct).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException ex)
            {
                return StatusCode(500, $"Bir hata oluştu: {ex.Message}");
            }

            return NoContent();
        }

        // DELETE /api/products/{id}: Bir ürünü silme
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            var product = await _context.Products.FindAsync(id);

            if (product == null)
            {
                return NotFound("Ürün bulunamadı.");
            }

            _context.Products.Remove(product);
            await _context.SaveChangesAsync();

            return Ok("Ürün başarıyla silindi.");
        }
    }
}
