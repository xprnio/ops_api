using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using OPS_API.Domain.Models;

namespace OPS_API.Domain.Repositories
{
    public interface IEquipmentRepository : IGenericRepository<Equipment>
    {
        Task<IEnumerable<EquipmentRequest>> FindAllRequestsAsync(Expression<Func<EquipmentRequest, bool>> predicate);
    }
}