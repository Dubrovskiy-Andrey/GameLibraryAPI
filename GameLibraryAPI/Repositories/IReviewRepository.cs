using GameLibraryAPI.Models;

namespace GameLibraryAPI.Repositories
{
    public interface IReviewRepository : IRepository<Review>
    {
        IEnumerable<Review> GetReviewsByGame(int gameId);
        IEnumerable<Review> GetReviewsByUser(int userId);
    }
}
