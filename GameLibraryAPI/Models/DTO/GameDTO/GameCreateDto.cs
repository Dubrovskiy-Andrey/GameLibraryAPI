namespace GameLibraryAPI.Models.DTO.GameDTO
{
    public class GameCreateDto
    {
        public string Title { get; set; } = null!;
        public string Description { get; set; } = null!;
        public string Tag { get; set; } = null!;
        public DateTime ReleaseDate { get; set; }
        public int GenreId { get; set; }
        public int PlatformId { get; set; }
    }
}
