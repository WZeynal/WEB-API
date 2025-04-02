using API_Homework.Data;
using API_Homework.Entities;
using API_Homework.Repositories.Abstract;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace API_Homework.Repositories.Concrete
{
    public class ProductRepository : IProductRepository
    {
        private readonly AppDbContext _context;

        public ProductRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Product> Add(Product entity)
        {
            await _context.Products.AddAsync(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<bool> Delete(Product entity)
        {
            await Task.Run(() =>
            {
                _context.Products.Remove(entity);
            });
            return await _context.SaveChangesAsync(true) > 0;
        }

        public async Task<Product> Get(Expression<Func<Product, bool>> predicate)
        {
            return await _context.Products.FirstOrDefaultAsync(predicate);
        }

        public async Task<IEnumerable<Product>> GetAll()
        {
            return await _context.Products.ToListAsync();
        }

        public async Task Update(Product entity)
        {
            await Task.Run(() =>
            {
                _context.Products.Update(entity);
            });
            await _context.SaveChangesAsync();
        }
    }
}
