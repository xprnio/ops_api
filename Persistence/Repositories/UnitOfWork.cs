using System.Threading.Tasks;
using OPS_API.Domain.Repositories;
using OPS_API.Persistence.Contexts;

namespace OPS_API.Persistence.Repositories
{
    public class UnitOfWork : BaseRepository, IUnitOfWork
    {
        public UnitOfWork(ApplicationContext context) : base(context) { }

        public async Task CompleteTask()
        {
            await _context.SaveChangesAsync();
        }
    }
}