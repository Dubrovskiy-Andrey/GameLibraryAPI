using GameLibraryAPI.Models;

namespace GameLibraryAPI.Repositories
{
    public class GenreRepository : IGenreRepository
    {
        private readonly GameLibraryDBContext _context;
        public GenreRepository(GameLibraryDBContext context)
        {
            _context = context;
        }

        public Genre Create(Genre entity)
        {
            _context.Genres.Add(entity);
            _context.SaveChanges();
            return entity;
        }

        public bool Delete(int id)
        {
            var genre = GetById(id);
            if (genre == null) return false;

            _context.Genres.Remove(genre);
            _context.SaveChanges();
            return true;
        }

        public bool Exists(int id) => _context.Genres.Any(x => x.Id == id);

        public IEnumerable<Genre> GetAll() => _context.Genres.ToList();

        public Genre? GetById(int id) => _context.Genres.FirstOrDefault(x => x.Id == id);

        public Genre? GetByName(string name) => _context.Genres.FirstOrDefault(x => x.Name == name);

        public Genre Update(Genre entity)
        {
            _context.Genres.Update(entity);
            _context.SaveChanges();
            return entity;
        }
    }
}
