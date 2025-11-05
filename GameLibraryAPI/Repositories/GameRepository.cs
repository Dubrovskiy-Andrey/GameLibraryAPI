using GameLibraryAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace GameLibraryAPI.Repositories
{
    public class GameRepository : IGameRepository
    {
        private readonly GameLibraryDBContext _context;

        public GameRepository(GameLibraryDBContext context)
        {
            _context = context;
        }

        public Game Create(Game entity)
        {
            _context.Games.Add(entity);
            _context.SaveChanges();
            return entity;
        }

        public bool Delete(int id)
        {
            var game = GetById(id);
            if (game == null)
                return false;

            _context.Games.Remove(game);
            _context.SaveChanges();
            return true;
        }

        public bool Exists(int id)
        {
            return _context.Games.Any(x => x.Id == id);
        }

        public IEnumerable<Game> GetAll()
        {
            return _context.Games
                .Include(g => g.Genre)
                .Include(g => g.Platform)
                .ToList();
        }

        public Game? GetById(int id)
        {
            return _context.Games
                .Include(g => g.Genre)
                .Include(g => g.Platform)
                .FirstOrDefault(x => x.Id == id);
        }

        public IEnumerable<Game> GetGamesByGenre(int genreId)
        {
            return _context.Games
                .Include(g => g.Genre)
                .Include(g => g.Platform)
                .Where(g => g.GenreId == genreId)
                .ToList();
        }

        public IEnumerable<Game> GetGamesByPlatform(int platformId)
        {
            return _context.Games
                .Include(g => g.Genre)
                .Include(g => g.Platform)
                .Where(g => g.PlatformId == platformId)
                .ToList();
        }

        public Game Update(Game entity)
        {
            _context.Games.Update(entity);
            _context.SaveChanges();
            return entity;
        }
    }
}
