namespace GameLibraryAPI.Models.DTO.GameDTO
{
    public class GameDto
    {
        public int Id { get; set; }
        public string Title { get; set; } = null!;
        public string Description { get; set; } = null!;
        public string Tag { get; set; } = null!;
        public string GenreName { get; set; } = null!;
        public string PlatformName { get; set; } = null!;
        public DateTime ReleaseDate { get; set; }
    }
}
