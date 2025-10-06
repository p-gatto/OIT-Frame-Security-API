namespace OIT_Frame_Security_API.Services.Interfaces
{
    public interface IAuthService
    {
        Task<LoginResponse?> LoginAsync(LoginRequest request);
        Task<User?> GetUserByIdAsync(int userId);
        Task<bool> ValidateTokenAsync(string token);
    }
}
