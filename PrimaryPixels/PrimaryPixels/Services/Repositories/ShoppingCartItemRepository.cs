using Microsoft.EntityFrameworkCore;
using PrimaryPixels.Data;
using PrimaryPixels.Models.Products;
using PrimaryPixels.Models.ShoppingCartItem;

namespace PrimaryPixels.Services.Repositories
{
    public class ShoppingCartItemRepository : IShoppingCartItemRepository
    {
        private readonly PrimaryPixelsContext _context;

        public ShoppingCartItemRepository(PrimaryPixelsContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<ShoppingCartItem>> GetAll()
        {
            var items = await _context.ShoppingCartItems.Include(item => item.Product).ToArrayAsync();
            return items ?? throw new KeyNotFoundException("Unable to retrieve shopping cart items from database!");
        }

        public async Task<ShoppingCartItem> GetById(int id)
        {
            ShoppingCartItem? item = await _context.ShoppingCartItems.Include(item => item.Product).FirstOrDefaultAsync(s => s.Id == id);
            return item ?? throw new KeyNotFoundException($"Unable to retrieve shopping cart item from database with ID { id }!");
        }
        public async Task<int> Add(ShoppingCartItem entity)
        {
            await _context.ShoppingCartItems.AddAsync(entity);
            await _context.SaveChangesAsync();
            return entity.Id;
        }
        public async Task<int> Update(ShoppingCartItem entity)
        {
            _context.ShoppingCartItems.Update(entity);
            await _context.SaveChangesAsync();
            return entity.Id;
        }
        public async Task<int> DeleteById(int id)
        {
            var item = await _context.ShoppingCartItems.FirstOrDefaultAsync(i => i.Id == id);
            if (item == null) throw new KeyNotFoundException($"No shopping cart item found with ID {id}.");
            _context.ShoppingCartItems.Remove(item);
            await _context.SaveChangesAsync();
            return item.Id;
        }

        public async Task<IEnumerable<ShoppingCartItem>> GetByUserId(string id)
        {
            var cartItems = await _context.ShoppingCartItems
                .Include(item => item.Product)
                .Where(item => item.UserId == id)
                .ToListAsync();
            if(cartItems == null) throw new KeyNotFoundException($"No shopping cart item found with this user: {id}.");
            return cartItems;
        }

        public async Task<bool> DeleteByUserId(string id)
        {
            var items = await _context.ShoppingCartItems.Where(s => s.UserId == id).ToListAsync();
            if (!items.Any())
                return false; 
            _context.ShoppingCartItems.RemoveRange(items);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
