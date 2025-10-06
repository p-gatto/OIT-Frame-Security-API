namespace OIT_Frame_Security_API.Services.Interfaces
{
    public interface IMenuService
    {
        Task<MenuResponse> GetMenuForUserAsync(int userId);
        Task<List<MenuItemDto>> GetAllMenuItemsAsync();
    }
}
