using API_Homework.Data;
using API_Homework.Entities;
using API_Homework.Repositories.Abstract;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace API_Homework.Repositories.Concrete
{
    public class OrderRepository : IOrderRepository
    {
        private readonly AppDbContext _context;

        public OrderRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Order> Add(Order entity)
        {
            await _context.Orders.AddAsync(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<bool> Delete(Order entity)
        {
            await Task.Run(() =>
            {
                _context.Orders.Remove(entity);
            });
            return await _context.SaveChangesAsync(true) > 0;
        }

        public async Task<Order> Get(Expression<Func<Order, bool>> predicate)
        {
            return await _context.Orders.FirstOrDefaultAsync(predicate);
        }

        public async Task<IEnumerable<Order>> GetAll()
        {
            return await _context.Orders.ToListAsync();
        }

        public async Task Update(Order entity)
        {
            await Task.Run(() =>
            {
                _context.Orders.Update(entity);
            });
            await _context.SaveChangesAsync();
        }
    }
}
