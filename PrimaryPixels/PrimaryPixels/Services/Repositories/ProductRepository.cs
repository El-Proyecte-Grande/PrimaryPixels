using Microsoft.EntityFrameworkCore;
using PrimaryPixels.Data;
using PrimaryPixels.Models.Products;

namespace PrimaryPixels.Services.Repositories
{
    public class ProductRepository<T> : Repository<T> where T : Product
    {
        private readonly PrimaryPixelsContext _context;
        public ProductRepository(PrimaryPixelsContext context)
        {
            _context = context;
        }
        public override async Task<IEnumerable<T>> GetAll()
        {
            var products = await _context.Products.OfType<T>().ToArrayAsync();
            if (products == null) throw new KeyNotFoundException("Unable to retrieve products from database!");
            return products;
        }

        public override async Task<T> GetById(int id)
        {
            T? product = (T?)await _context.Products.SingleOrDefaultAsync(p => p.Id == id);
            if (product == null) throw new KeyNotFoundException($"Unable to retrieve product from database with ID {id}!");
            return product;
        }
        public override async Task<int> Add(T entity)
        {
            await _context.Products.AddAsync(entity);
            await _context.SaveChangesAsync();
            return entity.Id;
        }

        public override async Task<int> Update(T entity)
        {
            _context.Products.Update(entity);
            await _context.SaveChangesAsync();
            return entity.Id;
        }
        public override async Task<int> DeleteById(int id)
        {
            var product = await _context.Products.FirstOrDefaultAsync(p => p.Id == id);
            if (product == null) throw new KeyNotFoundException($"No product found with ID {id}.");
            _context.Products.Remove(product);
            await _context.SaveChangesAsync();
            return id;
        }

    }
}
