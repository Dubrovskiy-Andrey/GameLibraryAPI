using GameLibraryAPI.Models;

namespace GameLibraryAPI.Repositories
{
    public class RoleRepository : IRoleRepository
    {
        private readonly GameLibraryDBContext _context;
        public RoleRepository(GameLibraryDBContext context)
        {
            _context = context;
        }

        public Role Create(Role entity)
        {
            _context.Roles.Add(entity);
            _context.SaveChanges();
            return entity;
        }

        public bool Delete(int id)
        {
            var role = GetById(id);
            if (role == null) return false;

            _context.Roles.Remove(role);
            _context.SaveChanges();
            return true;
        }

        public bool Exists(int id) => _context.Roles.Any(x => x.Id == id);

        public IEnumerable<Role> GetAll() => _context.Roles.ToList();

        public Role? GetById(int id) => _context.Roles.FirstOrDefault(x => x.Id == id);

        public Role? GetByName(string name) => _context.Roles.FirstOrDefault(x => x.Name == name);

        public Role Update(Role entity)
        {
            _context.Roles.Update(entity);
            _context.SaveChanges();
            return entity;
        }
    }
}
