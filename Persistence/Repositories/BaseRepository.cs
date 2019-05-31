using OPS_API.Persistence.Contexts;

namespace OPS_API.Persistence.Repositories
{
    public abstract class BaseRepository
    {
        protected ApplicationContext _context;

        public BaseRepository(ApplicationContext context)
        {
            _context = context;
        }
    }
}