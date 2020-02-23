using Microsoft.EntityFrameworkCore;

namespace Contoso.Apps.SportsLeague.Data.Models
{
    // Helpful Links:
    // https://docs.microsoft.com/en-us/ef/core/miscellaneous/configuring-dbcontext

  public class ProductContext : DbContext
  {
    public ProductContext(DbContextOptions<ProductContext> options)
            : base(options)
    {
    }

    public DbSet<Category> Categories { get; set; }
    public DbSet<Product> Products { get; set; }
    public DbSet<CartItem> ShoppingCartItems { get; set; }
    public DbSet<Order> Orders { get; set; }
    public DbSet<OrderDetail> OrderDetails { get; set; }
  }
}