using System;
using System.ComponentModel.DataAnnotations;

namespace OrderProductAPI.Models
{
    public partial class Product : BaseEntity
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Ürün adı zorunludur.")]
        public string Name { get; set; } = null!;

        [Range(0.01, double.MaxValue, ErrorMessage = "Fiyat sıfırdan küçük olamaz.")]
        public decimal Price { get; set; }

        [Range(0, int.MaxValue, ErrorMessage = "Stok negatif olamaz.")]
        public int Stock { get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime UpdatedAt { get; set; }

        public virtual ICollection<Order> Orders { get; set; } = new List<Order>();
    }
}

