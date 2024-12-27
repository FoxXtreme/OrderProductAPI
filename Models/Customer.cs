namespace OrderProductAPI.Models
{
    using System;

    namespace OrderProductAPICustomer.Models
    {
        public class Customer : BaseEntity
        {
            public string CustomerName { get; set; } // Müşteri adı, boş olamaz
            public string CustomerEmail { get; set; } // Müşteri e-posta, boş olamaz
            public DateTime CreatedAt { get; set; }
            public DateTime UpdatedAt { get; set; }
        }
    }

}
