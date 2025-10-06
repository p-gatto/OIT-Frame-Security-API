namespace OIT_Frame_Security_API.Models.Dtos
{
    public class LoginRequest
    {
        public required string Username { get; set; }
        public required string Password { get; set; }
    }
}
