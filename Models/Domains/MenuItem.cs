namespace OIT_Frame_Security_API.Models.Domains
{
    public class MenuItem
    {
        public int Id { get; set; }
        public required string Icon { get; set; }
        public required string Label { get; set; }
        public required string Route { get; set; }
        public required string Section { get; set; } // 'backoffice' or 'frontoffice'
        public int Order { get; set; }
        public bool IsActive { get; set; } = true;

        // Navigation properties
        public ICollection<RoleMenuItem> RoleMenuItems { get; set; } = new List<RoleMenuItem>();
    }
}
