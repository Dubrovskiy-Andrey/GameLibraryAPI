using System.ComponentModel.DataAnnotations;

namespace GameLibraryAPI.Models.DTO.UserDTO
{
    public class CreateUserRequest
    {
        [Required]
        [StringLength(50)]
        public string Login { get; set; } = null!;
        [Required]
        [StringLength(100)]
        public string Email { get; set; } = null!;
        [Required]
        [MinLength(8)]
        public string Password { get; set; } = null!;
        public int RoleId { get; set; } = 2;
    }
}
