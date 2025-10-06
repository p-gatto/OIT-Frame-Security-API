namespace OIT_Frame_Security_API.Models.Domains
{
    public class Role
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public string? Description { get; set; }

        // Navigation properties
        public ICollection<UserRole> UserRoles { get; set; } = new List<UserRole>();
        public ICollection<RoleMenuItem> RoleMenuItems { get; set; } = new List<RoleMenuItem>();
    }
}
