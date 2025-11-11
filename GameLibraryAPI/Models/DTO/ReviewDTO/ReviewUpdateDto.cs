namespace GameLibraryAPI.Models.DTO.ReviewDTO
{
    public class ReviewUpdateDto
    {
        public int Rating { get; set; }
        public string Comment { get; set; } = null!;
    }
}
