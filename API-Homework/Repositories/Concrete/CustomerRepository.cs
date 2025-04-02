using API_Homework.Data;
using API_Homework.Entities;
using API_Homework.Repositories.Abstract;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace API_Homework.Repositories.Concrete
{
    public class CustomerRepository : ICustomerRepository
    {
        private readonly AppDbContext _context;

        public CustomerRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Customer> Add(Customer entity)
        {
            await _context.Customers.AddAsync(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<bool> Delete(Customer entity)
        {
            await Task.Run(() =>
            {
                _context.Customers.Remove(entity);
            });
            return await _context.SaveChangesAsync(true) > 0;
        }

        public async Task<Customer> Get(Expression<Func<Customer, bool>> predicate)
        {
            return await _context.Customers.FirstOrDefaultAsync(predicate);
        }

        public async Task<IEnumerable<Customer>> GetAll()
        {
            return await _context.Customers.ToListAsync();
        }

        public async Task Update(Customer entity)
        {
            await Task.Run(() =>
            {
                _context.Customers.Update(entity);
            });
            await _context.SaveChangesAsync();
        }
    }
}
