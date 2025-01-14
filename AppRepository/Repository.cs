using Microsoft.EntityFrameworkCore;
using MVCDesignPatternsApp.AppContext;
using MVCDesignPatternsApp.IAppRepository;
using System.Collections.Generic;

namespace MVCDesignPatternsApp.AppRepository
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly ApplicationDbContext _context;
        private DbSet<T> entities;

        public Repository(ApplicationDbContext context)
        {
            _context = context;
            entities = context.Set<T>();
        }

        public IEnumerable<T> GetAll() => entities.ToList();
        public T GetById(int id) => entities.Find(id);
        public void Insert(T entity) => entities.Add(entity);
        public void Update(T entity) => _context.Entry(entity).State = EntityState.Modified;
        public void Delete(int id) => entities.Remove(GetById(id));
    }
}
