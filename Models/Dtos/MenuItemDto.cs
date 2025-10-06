namespace OIT_Frame_Security_API.Models.Dtos
{
    public class MenuItemDto
    {
        public int Id { get; set; }
        public required string Icon { get; set; }
        public required string Label { get; set; }
        public required string Route { get; set; }
        public required string Section { get; set; }
    }
}
