using System;
using System.Collections.Generic;

namespace OrderProductAPI.Models;

public partial class Order : BaseEntity
{
    public int Id { get; set; }

    public int ProductId { get; set; }

    public int Quantity { get; set; }

    public DateTime OrderDate { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }

    public virtual Product Product { get; set; }
}
