using Microsoft.EntityFrameworkCore;
using EComWebApi.Models;

namespace EComWebApi.Data {
    public class EComDbContext : DbContext {
        public EComDbContext(DbContextOptions<EComDbContext> options) : base(options) { }

        // DbSets for your models
        public DbSet<UserProfile> UserProfiles { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Review> Reviews { get; set; }
        public DbSet<WishList> WishLists { get; set; }
        
        public DbSet<BasketProduct> BasketProducts { get; set; }

        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }

        public DbSet<Ticket> Tickets { get; set; }
    }
}