using Microsoft.EntityFrameworkCore;
using PrimaryPixels.Data;
using PrimaryPixels.Models.Order;

namespace PrimaryPixels.Services.Repositories;

public class OrderDetailsRepository :  IOrderDetailsRepository
{
    private readonly PrimaryPixelsContext _context;
    public OrderDetailsRepository(PrimaryPixelsContext context)
    {
        _context = context;
    }
    
    public  async Task<IEnumerable<OrderDetails>> GetAll()
    {
        return await _context.OrderDetails.ToArrayAsync();
    }

    public  async  Task<OrderDetails> GetById(int id)
    {
        var orderDetail = await _context.OrderDetails.FirstOrDefaultAsync(o => o.Id == id);
        if(orderDetail == null) throw new KeyNotFoundException();
        return orderDetail;
    }

    public async  Task<int> Add(OrderDetails entity)
    {
        await _context.OrderDetails.AddAsync(entity);
        await _context.SaveChangesAsync();
        return entity.Id;
    }

    public async  Task<int> Update(OrderDetails entity)
    {
        _context.OrderDetails.Update(entity);
        await _context.SaveChangesAsync();
        return entity.Id;
    }

    public async  Task<int> DeleteById(int id)
    {
        var orderdetail = await _context.OrderDetails.FirstOrDefaultAsync(o => o.Id == id);
        if(orderdetail == null) throw new KeyNotFoundException();
        _context.OrderDetails.Remove(orderdetail);
        await _context.SaveChangesAsync();
        return id;
    }
    

    public async Task<IEnumerable<OrderDetails>> GetProductsForOrder(int orderId)
    {
        return await _context.OrderDetails.Where(o => o.OrderId == orderId).ToListAsync();
    }
}