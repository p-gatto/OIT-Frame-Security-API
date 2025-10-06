namespace OIT_Frame_Security_API.Models.Dtos
{
    public class LoginResponse
    {
        public required string Token { get; set; }
        public required string Username { get; set; }
        public required string Email { get; set; }
        public required string FirstName { get; set; }
        public required string LastName { get; set; }
        public required List<string> Roles { get; set; }
        public DateTime ExpiresAt { get; set; }
    }
}
