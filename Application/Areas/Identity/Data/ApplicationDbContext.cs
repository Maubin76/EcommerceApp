using Application.Areas.Identity.Data;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Application.Models;

namespace Application.Areas.Identity.Data;

public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }
    public DbSet<Item> Items {get;set;}
    public DbSet<Cart> Carts { get; set; }
    public DbSet<Review> Reviews{get; set;}
    public DbSet<CartItem> CartItems { get; set; } // Entité de liaison

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        // Customize the ASP.NET Identity model and override the defaults if needed.
        // For example, you can rename the ASP.NET Identity table names and more.
        // Add your customizations after calling base.OnModelCreating(builder);
        builder.ApplyConfiguration(new ApplicationUserEntityConfiguration());

        //Configuration for CartItem
        builder.Entity<CartItem>()
            .HasKey(ci => new { ci.cartId, ci.itemId }); // Composite Key

        //Configuration for many-to-many relation with Items and Cart
        builder.Entity<CartItem>()
            .HasOne(ci => ci.cart)
            .WithMany(c => c.cartItems)
            .HasForeignKey(ci => ci.cartId);

        builder.Entity<CartItem>()
            .HasOne(ci => ci.item)
            .WithMany()
            .HasForeignKey(ci => ci.itemId);

        // Configuration for Cart
        builder.Entity<Cart>()
        .HasKey(c => c.id); // Primary key for Cart

        // Configuration for Item
        builder.Entity<Item>()
        .HasKey(i => i.id); // Primary key Item
        
        //Configuration for Review
        builder.Entity<Review>()
        .HasKey(r=>r.id); // Primary key Review
    }

    public List<CartItem> GetAllOdersFromUser(String UserId)
    {
        // Retrieve all CartItems related to a user's carts
        var orders = CartItems
            .Where(ci => ci.cart.userId == UserId) // Filter by UserId in Cart
            .ToList();

        return orders;
    }
}

public class ApplicationUserEntityConfiguration : IEntityTypeConfiguration<ApplicationUser>
{
    void IEntityTypeConfiguration<ApplicationUser>.Configure(EntityTypeBuilder<ApplicationUser> builder)
    {
        builder.Property(x => x.FirstName).HasMaxLength(50);
        builder.Property(x => x.LastName).HasMaxLength(50);
    }
}



    

   

