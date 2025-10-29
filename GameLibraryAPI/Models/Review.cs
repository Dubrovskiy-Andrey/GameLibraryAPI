using System.Numerics;

namespace GameLibraryAPI.Models
{
    public class Review
    {
        public int Id { get; set; }
        public int Rating { get; set; }
        public string Comment { get; set; } = null!;
        public DateTime CreatedAt { get; set; }

        public int GameId { get; set; }
        public Game Game { get; set; } = null!;

        public int  UserId { get; set; }
        public User User { get; set; } = null!;
    }
}
