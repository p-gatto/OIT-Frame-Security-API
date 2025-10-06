namespace OIT_Frame_Security_API.Models.Domains
{
    public class RoleMenuItem
    {
        public int RoleId { get; set; }
        public Role Role { get; set; } = null!;

        public int MenuItemId { get; set; }
        public MenuItem MenuItem { get; set; } = null!;
    }
}
