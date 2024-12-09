using Microsoft.EntityFrameworkCore;
using PrimaryPixels.Data;
using PrimaryPixels.Models.Users;

namespace PrimaryPixels.Services.Repositories;

public class UserRepository : Repository<User>
{
    private PrimaryPixelsContext _context;
    
    public UserRepository(PrimaryPixelsContext context)
    {
        _context = context;
    }
    public override async Task<IEnumerable<User>> GetAll()
    {
        return await _context.Users.ToArrayAsync();
    }

    public override async Task<User> GetById(int id)
    {
        var user = await _context.Users.FirstOrDefaultAsync(u => u.Id == id);
        if (user == null) throw new KeyNotFoundException();
        return user;
    }

    public override async Task<int> Add(User entity)
    {
        await _context.Users.AddAsync(entity);
        await _context.SaveChangesAsync();
        return entity.Id;
    }

    public override async Task<int> Update(User entity)
    {
        _context.Users.Update(entity);
        await _context.SaveChangesAsync();
        return entity.Id;
    }

    public override async Task<int> DeleteById(int id)
    {
        var user = await _context.Users.FirstOrDefaultAsync(u => u.Id == id);
        if (user == null)
            throw new KeyNotFoundException($"No user found with ID {id}.");
        _context.Users.Remove(user);
        await _context.SaveChangesAsync();
        return id;
    }
}