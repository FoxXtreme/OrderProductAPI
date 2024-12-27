namespace OrderProductAPI.Repositories
{
    // Repositories/CustomerRepository.cs
    using Microsoft.EntityFrameworkCore;
    using OrderProductAPI.Models.OrderProductAPICustomer.Models;
    using OrderProductAPI.Models;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using OrderProductAPI.Repositories;

    public class CustomerRepository : ICustomerRepository
    {
        private readonly OrderProductAPIContext _context;

        public CustomerRepository(OrderProductAPIContext context)
        {
            _context = context;
        }

        // Tüm müşterileri listeleme
        public async Task<List<Customer>> GetAllAsync()
        {
            return await _context.Customers.ToListAsync();  // DbSet<Customer> kullanarak tüm müşterileri alıyoruz
        }

        // ID'ye göre müşteri getirme
        public async Task<Customer> GetByIdAsync(int id)
        {
            return await _context.Customers.FirstOrDefaultAsync(c => c.Id == id);  // Belirtilen ID'ye sahip müşteri
        }

        // Yeni müşteri ekleme
        public async Task AddAsync(Customer customer)
        {
            await _context.Customers.AddAsync(customer);  // Yeni müşteri ekleniyor
            await _context.SaveChangesAsync();  // Değişiklikler veritabanına kaydediliyor
        }

        // Müşteri güncelleme
        public async Task UpdateAsync(Customer customer)
        {
            _context.Customers.Update(customer);  // Müşteri güncelleniyor
            await _context.SaveChangesAsync();  // Değişiklikler veritabanına kaydediliyor
        }

        // Müşteri silme
        public async Task DeleteAsync(int id)
        {
            var customer = await _context.Customers.FindAsync(id);  // Silinecek müşteri bulunuyor
            if (customer != null)
            {
                _context.Customers.Remove(customer);  // Müşteri siliniyor
                await _context.SaveChangesAsync();  // Değişiklikler veritabanına kaydediliyor
            }
        }
    }

}
