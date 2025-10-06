namespace OIT_Frame_Security_API.Models.Dtos
{
    public class MenuResponse
    {
        public required List<MenuItemDto> BackOfficeItems { get; set; }
        public required List<MenuItemDto> FrontOfficeItems { get; set; }
    }
}
