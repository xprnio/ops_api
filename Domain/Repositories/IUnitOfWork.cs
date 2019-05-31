using System.Threading.Tasks;

namespace OPS_API.Domain.Repositories
{
    public interface IUnitOfWork
    {
        Task CompleteTask();
    }
}