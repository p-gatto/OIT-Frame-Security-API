using Microsoft.EntityFrameworkCore;

namespace OIT_Frame_Security_API.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<MenuItem> MenuItems { get; set; }
        public DbSet<UserRole> UserRoles { get; set; }
        public DbSet<RoleMenuItem> RoleMenuItems { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // User configuration
            modelBuilder.Entity<User>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.HasIndex(e => e.Username).IsUnique();
                entity.HasIndex(e => e.Email).IsUnique();
                entity.Property(e => e.Username).HasMaxLength(50).IsRequired();
                entity.Property(e => e.Email).HasMaxLength(100).IsRequired();
                entity.Property(e => e.FirstName).HasMaxLength(50).IsRequired();
                entity.Property(e => e.LastName).HasMaxLength(50).IsRequired();
            });

            // Role configuration
            modelBuilder.Entity<Role>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.HasIndex(e => e.Name).IsUnique();
                entity.Property(e => e.Name).HasMaxLength(50).IsRequired();
                entity.Property(e => e.Description).HasMaxLength(200);
            });

            // MenuItem configuration
            modelBuilder.Entity<MenuItem>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Icon).HasMaxLength(50).IsRequired();
                entity.Property(e => e.Label).HasMaxLength(100).IsRequired();
                entity.Property(e => e.Route).HasMaxLength(200).IsRequired();
                entity.Property(e => e.Section).HasMaxLength(20).IsRequired();
            });

            // UserRole many-to-many configuration
            modelBuilder.Entity<UserRole>(entity =>
            {
                entity.HasKey(ur => new { ur.UserId, ur.RoleId });

                entity.HasOne(ur => ur.User)
                    .WithMany(u => u.UserRoles)
                    .HasForeignKey(ur => ur.UserId)
                    .OnDelete(DeleteBehavior.Cascade);

                entity.HasOne(ur => ur.Role)
                    .WithMany(r => r.UserRoles)
                    .HasForeignKey(ur => ur.RoleId)
                    .OnDelete(DeleteBehavior.Cascade);
            });

            // RoleMenuItem many-to-many configuration
            modelBuilder.Entity<RoleMenuItem>(entity =>
            {
                entity.HasKey(rm => new { rm.RoleId, rm.MenuItemId });

                entity.HasOne(rm => rm.Role)
                    .WithMany(r => r.RoleMenuItems)
                    .HasForeignKey(rm => rm.RoleId)
                    .OnDelete(DeleteBehavior.Cascade);

                entity.HasOne(rm => rm.MenuItem)
                    .WithMany(m => m.RoleMenuItems)
                    .HasForeignKey(rm => rm.MenuItemId)
                    .OnDelete(DeleteBehavior.Cascade);
            });
        }
    }
}
