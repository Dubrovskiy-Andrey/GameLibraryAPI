namespace GameLibraryAPI.Models.DTO.ReviewDTO
{
    public class ReviewCreateDto
    {
        public int Rating { get; set; }
        public string Comment { get; set; } = null!;
        public int GameId { get; set; }
        public int UserId { get; set; }
    }
}
