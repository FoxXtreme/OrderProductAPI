namespace OrderProductAPI.Models
{
    // Log.cs
    using System;

    public class Log : BaseEntity
    {
        public int Id { get; set; }
        public string Message { get; set; }
        public DateTime CreatedAt { get; set; }
    }

}
