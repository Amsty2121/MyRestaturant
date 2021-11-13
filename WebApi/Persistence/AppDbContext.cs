using Domain.Entities;
using Domain.Entities.Authorization;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Persistence.Constants;
using Role = Domain.Entities.Authorization.Role;
using User = Domain.Entities.Authorization.User;
namespace Persistence
{
    public class AppDbContext : IdentityDbContext<User, Role, int, UserClaim, UserRole, UserLogin, RoleClaim, UserToken>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);

            ApplyIdentityMapConfiguration(modelBuilder);
        }

        public DbSet<UserDetails> UserDetails { get; set; }
        public DbSet<TableStatus> TableStatuses { get; set; }
        public DbSet<Table> Tables { get; set; }
        public DbSet<OrderStatus> OrderStatuses { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<IngredientStatus> IngredientStatuses { get; set; }
        public DbSet<Ingredient> Ingredients { get; set; }
        public DbSet<DishStatus> DishStatuses { get; set; }
        public DbSet<DishCategory> DishCategories { get; set; }
        public DbSet<Dish> Dishes { get; set; }
        public DbSet<DishIngredient> DishIngredients { get; set; }

        private void ApplyIdentityMapConfiguration(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().ToTable("Users", SchemaConstants.Auth);
            modelBuilder.Entity<UserClaim>().ToTable("UserClaims", SchemaConstants.Auth);
            modelBuilder.Entity<UserLogin>().ToTable("UserLogins", SchemaConstants.Auth);
            modelBuilder.Entity<UserToken>().ToTable("UserRoles", SchemaConstants.Auth);
            modelBuilder.Entity<Role>().ToTable("Roles", SchemaConstants.Auth);
            modelBuilder.Entity<RoleClaim>().ToTable("RoleClaims", SchemaConstants.Auth);
            modelBuilder.Entity<UserRole>().ToTable("UserRole", SchemaConstants.Auth);
        }
    }

    
}
