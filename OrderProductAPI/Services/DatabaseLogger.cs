using OrderProductAPI.Models;
using System;
using System.Threading.Tasks;

namespace OrderProductAPI.Services
{
    public class DatabaseLogger : ILogger
    {
        private readonly OrderProductAPIContext _context;

        // DbContext injection ile sınıfa dahil ediliyor
        public DatabaseLogger(OrderProductAPIContext context)
        {
            _context = context;
        }

        // Log mesajını veritabanına yazan metod
        public async void Log(string message)
        {
            try
            {
                var logEntry = new Log
                {
                    Message = message,
                    CreatedAt = DateTime.UtcNow // UTC zaman diliminde log kaydı
                };

                // Log veritabanına kaydediliyor
                await _context.Logs.AddAsync(logEntry);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error while logging to database: {ex.Message}");
            }
        }
    }
}
