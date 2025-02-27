using Microsoft.EntityFrameworkCore;
using PrimaryPixels.Data;
using PrimaryPixels.Models.Order;

namespace PrimaryPixels.Services.Repositories;

public class OrderRepository : Repository<Order>, IOrderRepository
{
    private readonly PrimaryPixelsContext _context;
    public OrderRepository(PrimaryPixelsContext context)
    {
        _context = context;
    }
    public async override  Task<IEnumerable<Order>> GetAll()
    {
        return await _context.Orders.ToArrayAsync();
    }

    public async override Task<Order> GetById(int id)
    {
        var order = await _context.Orders.FirstOrDefaultAsync(o => o.Id == id);
        if(order == null) throw new KeyNotFoundException();
        return order;
    }

    public async override Task<int> Add(Order entity)
    {
        await _context.Orders.AddAsync(entity);
        await _context.SaveChangesAsync();
        return entity.Id;
    }

    public async override Task<int> Update(Order entity)
    {
        _context.Orders.Update(entity);
        await _context.SaveChangesAsync();
        return entity.Id;
    }

    public async override Task<int> DeleteById(int id)
    {
        var order = await _context.Orders.FirstOrDefaultAsync(o => o.Id == id);
        if(order == null) throw new KeyNotFoundException();
        _context.Orders.Remove(order);
        await _context.SaveChangesAsync();
        return id;
    }

    public async Task<IEnumerable<Order>> GetOrdersByUserId(string userId)
    {
        return await _context.Orders.Where(o => o.UserId == userId).ToListAsync();
    }

}