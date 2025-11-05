using GameLibraryAPI.Models;

namespace GameLibraryAPI.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly GameLibraryDBContext _context;
        public UserRepository(GameLibraryDBContext context)
        {
            _context = context;
        }

        public User Create(User entity)
        {
            _context.Users.Add(entity);
            _context.SaveChanges();
            return entity;
        }

        public bool Delete(int id)
        {
            var user = GetById(id);
            if (user == null) return false;

            _context.Users.Remove(user);
            _context.SaveChanges();
            return true;
        }

        public bool Exists(int id) => _context.Users.Any(x => x.Id == id);

        public IEnumerable<User> GetAll() => _context.Users.ToList();

        public User? GetById(int id) => _context.Users.FirstOrDefault(x => x.Id == id);

        public User? GetByEmail(string email) => _context.Users.FirstOrDefault(x => x.Email == email);

        public User Update(User entity)
        {
            _context.Users.Update(entity);
            _context.SaveChanges();
            return entity;
        }
    }
}
