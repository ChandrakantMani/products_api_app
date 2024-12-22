using Microsoft.EntityFrameworkCore;
using ProductsAPI.Models;
using System.Collections.Generic;
using System.Reflection.Emit;

namespace ProductsAPI.Data
{

    public class ProductDbContext : DbContext
    {
        public DbSet<Product> Products { get; set; }
        public ProductDbContext(DbContextOptions<ProductDbContext> options)
        : base(options)
        {
        }

        public override int SaveChanges()
        {
            UpdateTimestamps();
            return base.SaveChanges();
        }

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            UpdateTimestamps();
            return await base.SaveChangesAsync(cancellationToken);
        }

        private void UpdateTimestamps()
        {
            var entries = ChangeTracker.Entries<Product>();

            foreach (var entry in entries)
            {
                if (entry.State == EntityState.Added)
                {
                    entry.Entity.CreatedDate = DateTime.UtcNow;
                    entry.Entity.UpdatedDate = DateTime.UtcNow;
                }
                else if (entry.State == EntityState.Modified)
                {
                    entry.Entity.UpdatedDate = DateTime.UtcNow;
                }
            }
        }
    }


    //public class ProductDbContext : DbContext
    //{
    //    public DbSet<Product> Products { get; set; }

    //    public ProductDbContext(DbContextOptions<ProductDbContext> options)
    //        : base(options) { }

    //    protected override void OnModelCreating(ModelBuilder modelBuilder)
    //    {
    //        modelBuilder.Entity<Product>()
    //            .Property(p => p.CreatedDate)
    //            .HasDefaultValueSql("GETDATE()"); 
    //        modelBuilder.Entity<Product>()
    //            .Property(p => p.UpdatedDate)
    //            .HasDefaultValueSql("GETDATE()"); 
    //    }
    //    public override int SaveChanges()
    //    {
    //        UpdateTimestamps();
    //        return base.SaveChanges();
    //    }

    //    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    //    {
    //        UpdateTimestamps();
    //        return await base.SaveChangesAsync(cancellationToken);
    //    }

    //    private void UpdateTimestamps()
    //    {
    //        var entries = ChangeTracker.Entries()
    //            .Where(e => e.Entity is Product && (e.State == EntityState.Added || e.State == EntityState.Modified));

    //        foreach (var entry in entries)
    //        {
    //            var entity = (Product)entry.Entity;

    //            if (entry.State == EntityState.Added)
    //            {
    //                entity.CreatedDate = DateTime.Now; 
    //            }

    //            entity.UpdatedDate = DateTime.Now; 
    //        }
    //    }


    //}

}
