using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using OrderProductAPI.Models;
using OrderProductAPI.Models.OrderProductAPICustomer.Models;

namespace OrderProductAPI.Models;

public partial class OrderProductAPIContext : DbContext
{
    public OrderProductAPIContext()
    {
    }

    public OrderProductAPIContext(DbContextOptions<OrderProductAPIContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Order> Orders { get; set; }

    public virtual DbSet<Product> Products { get; set; }
    public DbSet<Customer> Customers { get; set; }

    public DbSet<Log> Logs { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseSqlServer("Data Source=localhost\\SQLEXPRESS;Initial Catalog=OrderProductDB;Integrated Security=True;Trust Server Certificate=True");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Order>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Orders__3214EC07F98611B9");

            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.OrderDate).HasColumnType("datetime");
            entity.Property(e => e.UpdatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");

            entity.HasOne(d => d.Product).WithMany(p => p.Orders)
                .HasForeignKey(d => d.ProductId)
                .HasConstraintName("FK_Orders_Products");
        });

        modelBuilder.Entity<Product>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Products__3214EC07F619542F");

            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.Name).HasMaxLength(50);
            entity.Property(e => e.Price).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.UpdatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
        });

        modelBuilder.Entity<Customer>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Customers__3214EC07F619542F"); // assuming Id is the primary key

            entity.Property(e => e.CustomerName)
                .IsRequired()  // assuming it's a required field
                .HasMaxLength(100);  // maximum length for customer name, adjust as needed

            entity.Property(e => e.CustomerEmail)
                .IsRequired()  // assuming it's a required field
                .HasMaxLength(150)  // maximum length for customer email, adjust as needed
                .HasColumnType("varchar(150)");

            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");

            entity.Property(e => e.UpdatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
        });

        // Log tablosu için model yapılandırması
        modelBuilder.Entity<Log>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_Logs");
            entity.Property(e => e.Message)
                .IsRequired()  // Mesaj alanı zorunlu
                .HasMaxLength(1000);  // Mesajın maksimum uzunluğu

            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")  // Varsayılan değer olarak şu anki tarih
                .HasColumnType("datetime");

            entity.Property(e => e.UpdatedAt)
                .HasDefaultValueSql("(getdate())")  // Varsayılan değer olarak şu anki tarih
                .HasColumnType("datetime");
        });



        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
