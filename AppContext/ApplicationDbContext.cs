using Microsoft.EntityFrameworkCore;

namespace MVCDesignPatternsApp.AppContext
{
    public class ApplicationDbContext : DbContext
    {

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }
    }
}
