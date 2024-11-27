using Microsoft.EntityFrameworkCore;
using PrimaryPixels.Data;
using PrimaryPixels.Models.Order;
using PrimaryPixels.Repositories;

namespace PrimaryPixels.Services.Repositories;

public class OrderRepository : Repository<Order>
{
    private PrimaryPixelsContext _context;
    public OrderRepository(PrimaryPixelsContext context)
    {
        _context = context;
    }
    public override async  Task<IEnumerable<Order>> GetAll()
    {
        return await _context.Orders.ToListAsync();
    }

    public override async Task<Order> GetById(int id)
    {
        var order = await _context.Orders.FirstOrDefaultAsync(o => o.Id == id);
        if(order == null) throw new KeyNotFoundException();
        return order;
    }

    public override async Task<int> Add(Order entity)
    {
        _context.Orders.Add(entity);
        await _context.SaveChangesAsync();
        return entity.Id;
    }

    public override async Task<int> Update(Order entity)
    {
        _context.Orders.Update(entity);
        await _context.SaveChangesAsync();
        return entity.Id;
    }

    public override async Task<int> DeleteById(int id)
    {
        var order = await _context.Orders.FirstOrDefaultAsync(o => o.Id == id);
        if(order == null) throw new KeyNotFoundException();
        _context.Orders.Remove(order);
        await _context.SaveChangesAsync();
        return id;
    }
}