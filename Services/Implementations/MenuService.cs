using Microsoft.EntityFrameworkCore;

namespace OIT_Frame_Security_API.Services.Implementations
{
    public class MenuService : IMenuService
    {
        private readonly ApplicationDbContext _context;

        public MenuService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<MenuResponse> GetMenuForUserAsync(int userId)
        {
            // Get user with roles
            var user = await _context.Users
                .Include(u => u.UserRoles)
                .ThenInclude(ur => ur.Role)
                .ThenInclude(r => r.RoleMenuItems)
                .ThenInclude(rm => rm.MenuItem)
                .FirstOrDefaultAsync(u => u.Id == userId);

            if (user == null)
            {
                return new MenuResponse
                {
                    BackOfficeItems = new List<MenuItemDto>(),
                    FrontOfficeItems = new List<MenuItemDto>()
                };
            }

            // Get all menu items accessible to user's roles
            var menuItems = user.UserRoles
                .SelectMany(ur => ur.Role.RoleMenuItems)
                .Select(rm => rm.MenuItem)
                .Where(m => m.IsActive)
                .Distinct()
                .OrderBy(m => m.Order)
                .Select(m => new MenuItemDto
                {
                    Id = m.Id,
                    Icon = m.Icon,
                    Label = m.Label,
                    Route = m.Route,
                    Section = m.Section
                })
                .ToList();

            return new MenuResponse
            {
                BackOfficeItems = menuItems.Where(m => m.Section == "backoffice").ToList(),
                FrontOfficeItems = menuItems.Where(m => m.Section == "frontoffice").ToList()
            };
        }

        public async Task<List<MenuItemDto>> GetAllMenuItemsAsync()
        {
            return await _context.MenuItems
                .Where(m => m.IsActive)
                .OrderBy(m => m.Section)
                .ThenBy(m => m.Order)
                .Select(m => new MenuItemDto
                {
                    Id = m.Id,
                    Icon = m.Icon,
                    Label = m.Label,
                    Route = m.Route,
                    Section = m.Section
                })
                .ToListAsync();
        }
    }
}
