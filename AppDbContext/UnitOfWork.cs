using MVCDesignPatternsApp.AppContext;
using MVCDesignPatternsApp.AppRepository;
using MVCDesignPatternsApp.IAppRepository;
using MVCDesignPatternsApp.Models;

namespace MVCDesignPatternsApp.AppDbContext
{
    public class UnitOfWork : IDisposable
    {
        private readonly ApplicationDbContext _context;
        private IRepository<Product> _productRepository;

        public UnitOfWork(ApplicationDbContext context)
        {
            _context = context;
        }

        public IRepository<Product> ProductRepository
        {
            get
            {
                return _productRepository ??= new Repository<Product>(_context);
            }
        }

        public void Save()
        {
            _context.SaveChanges();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
