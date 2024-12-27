namespace OrderProductAPI.Repositories
{
    using OrderProductAPI.Models.OrderProductAPICustomer.Models;
    // Repositories/ICustomerRepository.cs
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface ICustomerRepository
    {
        Task<List<Customer>> GetAllAsync();  // Tüm müşterileri listeleme
        Task<Customer> GetByIdAsync(int id);  // ID'ye göre müşteri getirme
        Task AddAsync(Customer customer);     // Yeni müşteri ekleme
        Task UpdateAsync(Customer customer);  // Müşteri güncelleme
        Task DeleteAsync(int id);             // Müşteri silme
    }

}
