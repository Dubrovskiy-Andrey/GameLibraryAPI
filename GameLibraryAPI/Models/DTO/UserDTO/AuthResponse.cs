namespace GameLibraryAPI.Models.DTO.UserDTO
{
    public class AuthResponse
    {
        public bool Success { get; set; }
        public string Token { get; set; } = string.Empty;
        public string RefreshToken { get; set; } = string.Empty;
        public DateTime ValidTo { get; set; }
        public UserDto User { get; set; } = new UserDto();
        public string ErrorMessage { get; set; } = string.Empty;
    }

}
