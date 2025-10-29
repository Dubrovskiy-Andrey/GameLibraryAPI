namespace GameLibraryAPI.Models
{
    public class Game
    {
        public int Id { get; set; }
        public string Title { get; set; } = null!;
        public string Description { get; set; } = null!;
        public DateTime ReleaseDate { get; set; }

        public int GenreId { get; set; }
        public Genre Genre { get; set; } = null!;

        public int PlatformId { get; set; }
        public Platform Platform { get; set; } = null!;

        public ICollection<Review> Reviews { get; set; } = new List<Review>();
    }
}
