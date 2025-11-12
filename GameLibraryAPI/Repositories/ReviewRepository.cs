using GameLibraryAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace GameLibraryAPI.Repositories
{
    public class ReviewRepository : IReviewRepository
    {
        private readonly GameLibraryDBContext _context;
        public ReviewRepository(GameLibraryDBContext context)
        {
            _context = context;
        }

        public Review Create(Review entity)
        {
            _context.Reviews.Add(entity);
            _context.SaveChanges();
            return entity;
        }

        public bool Delete(int id)
        {
            var review = GetById(id);
            if (review == null) return false;

            _context.Reviews.Remove(review);
            _context.SaveChanges();
            return true;
        }

        public bool Exists(int id) => _context.Reviews.Any(x => x.Id == id);

        public IEnumerable<Review> GetAll()
        {
                return _context.Reviews
                .Include(r => r.Game)
                .Include(r => r.User)
                .ToList();
        }

        public Review? GetById(int id)
        {
            return _context.Reviews
                .Include(r => r.Game)
                .Include(r => r.User)
                .FirstOrDefault(x => x.Id == id);
        }

        public IEnumerable<Review> GetReviewsByGame(int gameId)
        {
            return _context.Reviews
                .Where(r => r.GameId == gameId)
                .Include(r => r.User)
                .ToList();
        }

        public IEnumerable<Review> GetReviewsByUser(int userId)
        {
            return _context.Reviews
                .Where(r => r.UserId == userId)
                .Include(r => r.Game)
                .ToList();
        }

        public Review Update(Review entity)
        {
            _context.Reviews.Update(entity);
            _context.SaveChanges();
            return entity;
        }
    }
}
