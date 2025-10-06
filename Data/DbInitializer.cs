using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;
using System.Text;

namespace OIT_Frame_Security_API.Data
{
    public static class DbInitializer
    {
        public static async Task Initialize(ApplicationDbContext context)
        {
            await context.Database.EnsureCreatedAsync();

            // Check if data already exists
            if (await context.Roles.AnyAsync())
            {
                return; // DB has been seeded
            }

            // Create Roles
            var adminRole = new Role { Name = "Administrator", Description = "Full access to all features" };
            var userRole = new Role { Name = "User", Description = "Limited access to front-office features" };

            await context.Roles.AddRangeAsync(adminRole, userRole);
            await context.SaveChangesAsync();

            // Create Menu Items - BackOffice
            var backofficeItems = new List<MenuItem>
        {
            new() { Icon = "dashboard", Label = "Dashboard", Route = "/admin/dashboard", Section = "backoffice", Order = 1 },
            new() { Icon = "people", Label = "Utenti", Route = "/admin/users", Section = "backoffice", Order = 2 },
            new() { Icon = "settings", Label = "Impostazioni", Route = "/admin/settings", Section = "backoffice", Order = 3 },
            new() { Icon = "assessment", Label = "Report", Route = "/admin/reports", Section = "backoffice", Order = 4 }
        };

            // Create Menu Items - FrontOffice
            var frontofficeItems = new List<MenuItem>
        {
            new() { Icon = "home", Label = "Home", Route = "/home", Section = "frontoffice", Order = 1 },
            new() { Icon = "shopping_cart", Label = "Prodotti", Route = "/products", Section = "frontoffice", Order = 2 },
            new() { Icon = "contact_mail", Label = "Contatti", Route = "/contact", Section = "frontoffice", Order = 3 },
            new() { Icon = "info", Label = "Chi Siamo", Route = "/about", Section = "frontoffice", Order = 4 }
        };

            await context.MenuItems.AddRangeAsync(backofficeItems);
            await context.MenuItems.AddRangeAsync(frontofficeItems);
            await context.SaveChangesAsync();

            // Assign menu items to roles
            // Administrator has access to all items
            foreach (var item in backofficeItems.Concat(frontofficeItems))
            {
                await context.RoleMenuItems.AddAsync(new RoleMenuItem
                {
                    RoleId = adminRole.Id,
                    MenuItemId = item.Id
                });
            }

            // User has access only to frontoffice items
            foreach (var item in frontofficeItems)
            {
                await context.RoleMenuItems.AddAsync(new RoleMenuItem
                {
                    RoleId = userRole.Id,
                    MenuItemId = item.Id
                });
            }

            await context.SaveChangesAsync();

            // Create Users
            var adminUser = new User
            {
                Username = "admin",
                Email = "admin@oitframe.com",
                PasswordHash = HashPassword("Admin123!"),
                FirstName = "Admin",
                LastName = "User",
                IsActive = true
            };

            var normalUser = new User
            {
                Username = "user",
                Email = "user@oitframe.com",
                PasswordHash = HashPassword("User123!"),
                FirstName = "Normal",
                LastName = "User",
                IsActive = true
            };

            await context.Users.AddRangeAsync(adminUser, normalUser);
            await context.SaveChangesAsync();

            // Assign roles to users
            await context.UserRoles.AddAsync(new UserRole { UserId = adminUser.Id, RoleId = adminRole.Id });
            await context.UserRoles.AddAsync(new UserRole { UserId = normalUser.Id, RoleId = userRole.Id });

            await context.SaveChangesAsync();
        }

        private static string HashPassword(string password)
        {
            using var sha256 = SHA256.Create();
            var hashedBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
            return Convert.ToBase64String(hashedBytes);
        }
    }
}
