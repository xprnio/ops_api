using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using OPS_API.Domain.Models;
using OPS_API.Domain.Repositories;
using OPS_API.Persistence.Contexts;

namespace OPS_API.Persistence.Repositories
{
    public class EquipmentRepository : BaseRepository, IEquipmentRepository
    {
        public EquipmentRepository(ApplicationContext context) : base(context) { }

        public async Task<IEnumerable<Equipment>> ListAllAsync()
        {
            return await _context.Equipment.ToListAsync();
        }

        public async Task<IEnumerable<Equipment>> FindAllAsync(Expression<Func<Equipment, bool>> predicate)
        {
            return await _context.Equipment.Where(predicate).ToListAsync();
        }

        public Task<Equipment> FindAsync(Expression<Func<Equipment, bool>> predicate)
        {
            throw new NotImplementedException();
        }

        public Task<long> CountAsync(Expression<Func<Equipment, bool>> predicate)
        {
            throw new NotImplementedException();
        }

        public async Task<Equipment> FindByIdAsync(Guid id)
        {
            return await _context.Equipment.FindAsync(id);
        }

        public Task AddAsync(Equipment user)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<EquipmentRequest>> FindAllRequestsAsync(
            Expression<Func<EquipmentRequest, bool>> predicate)
        {
            return await _context.EquipmentRequests.Where(predicate).ToListAsync();
        }
    }
}