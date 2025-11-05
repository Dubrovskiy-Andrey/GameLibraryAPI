using GameLibraryAPI.Models;

namespace GameLibraryAPI.Repositories
{
    public class PlatformRepository : IPlatformRepository
    {
        private readonly GameLibraryDBContext _context;
        public PlatformRepository(GameLibraryDBContext context)
        {
            _context = context;
        }

        public Platform Create(Platform entity)
        {
            _context.Platforms.Add(entity);
            _context.SaveChanges();
            return entity;
        }

        public bool Delete(int id)
        {
            var platform = GetById(id);
            if (platform == null) return false;

            _context.Platforms.Remove(platform);
            _context.SaveChanges();
            return true;
        }

        public bool Exists(int id) => _context.Platforms.Any(x => x.Id == id);

        public IEnumerable<Platform> GetAll() => _context.Platforms.ToList();

        public Platform? GetById(int id) => _context.Platforms.FirstOrDefault(x => x.Id == id);

        public Platform? GetByName(string name) => _context.Platforms.FirstOrDefault(x => x.Name == name);

        public Platform Update(Platform entity)
        {
            _context.Platforms.Update(entity);
            _context.SaveChanges();
            return entity;
        }
    }
}
