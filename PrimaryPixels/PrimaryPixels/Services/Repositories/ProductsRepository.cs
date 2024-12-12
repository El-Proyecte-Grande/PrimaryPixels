using Microsoft.EntityFrameworkCore;
using PrimaryPixels.Data;
using PrimaryPixels.Models.Order;
using PrimaryPixels.Models.Products;

namespace PrimaryPixels.Services.Repositories;

public class ProductsRepository : IProductRepository
{
    private readonly PrimaryPixelsContext _context;
    public ProductsRepository(PrimaryPixelsContext context)
    {
        _context = context;
    }
    public async  Task<IEnumerable<Product>> GetAll()
    {
        return await _context.Products.ToArrayAsync();
    }

    public async Task<Product> GetById(int id)
    {
        var product = await _context.Products.FirstOrDefaultAsync(o => o.Id == id);
        if(product == null) throw new KeyNotFoundException();
        return product;
    }
}