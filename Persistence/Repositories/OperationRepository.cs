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
    public class OperationRepository : BaseRepository, IOperationRepository
    {
        public OperationRepository(ApplicationContext context) : base(context) { }

        public async Task<IEnumerable<Operation>> ListAllAsync()
        {
            return await _context.Operations.ToListAsync();
        }

        public async Task<IEnumerable<Operation>> FindAllAsync(Expression<Func<Operation, bool>> predicate)
        {
            return await _context.Operations.Where(predicate).ToListAsync();
        }

        public async Task<Operation> FindAsync(Expression<Func<Operation, bool>> predicate)
        {
            return await _context.Operations.SingleOrDefaultAsync(predicate);
        }

        public async Task<long> CountAsync(Expression<Func<Operation, bool>> predicate)
        {
            return await _context.Operations.Where(predicate).LongCountAsync();
        }

        public async Task<Operation> FindByIdAsync(Guid id)
        {
            return await _context.Operations.FindAsync(id);
        }

        public async Task AddAsync(Operation user)
        {
            await _context.Operations.AddAsync(user);
        }

        public async Task LoadRescuersAsync(Operation operation)
        {
            await _context.Entry(operation)
                .Collection(op => op.Rescuers)
                .LoadAsync();

            foreach (var rescuer in operation.Rescuers)
            {
                await _context.Entry(rescuer)
                    .Collection(r => r.Inventory)
                    .LoadAsync();

                foreach (var inventory in rescuer.Inventory)
                {
                    await _context.Entry(inventory)
                        .Reference(r => r.EquipmentRequest)
                        .LoadAsync();

                    await _context.Entry(inventory.EquipmentRequest)
                        .Reference(r => r.Equipment)
                        .LoadAsync();
                }
            }
        }

        public async Task LoadEquipmentAsync(Operation operation)
        {
            await _context.Entry(operation)
                .Collection(op => op.RequestedEquipment)
                .LoadAsync();

            foreach (var request in operation.RequestedEquipment)
            {
                await _context.Entry(request)
                    .Reference(r => r.Equipment)
                    .LoadAsync();
            }
        }
    }
}