using Microsoft.EntityFrameworkCore;
using OrderProductAPI.Repositories;
using OrderProductAPI.Services;
using OrderProductAPI.Models;
using OrderProductAPI.Repositories;
using OrderProductAPI.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<OrderProductAPIContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddScoped<OrderProductAPI.Services.ILogger, DatabaseLogger>();

builder.Services.AddScoped<ICustomerRepository, CustomerRepository>();

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer(); // Swagger için gerekli
builder.Services.AddSwaggerGen();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    // Swagger'ý JSON endpoint olarak servis ediyoruz
    app.UseSwagger();

    // Swagger UI'yý devreye alýyoruz (Swagger JSON endpoint ile)
    app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint("/swagger/v1/swagger.json", "API V1");
        options.RoutePrefix = string.Empty; // Swagger UI'nýn root URL'de (örneðin http://localhost:5078/) görünmesini saðlar
    });
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();

