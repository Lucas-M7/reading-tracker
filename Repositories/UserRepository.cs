using Microsoft.EntityFrameworkCore;
using ReadingTracker.API.Data;
using ReadingTracker.API.Entities;
using ReadingTracker.API.Entities.Identity;
using ReadingTracker.API.Repositories.Interfaces;

namespace ReadingTracker.API.Repositories;

public class UserRepository
{
    private readonly AppDbContext _context;

    public UserRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<ApplicationUser> CreateAsync(ApplicationUser user)
    {
        _context.Users.Add(user);
        await _context.SaveChangesAsync();
        return user;
    }

    public async Task<bool> DeleteAsync(Guid id)
    {
        var user = await _context.Users.FindAsync(id);
        if (user == null) return false;

        _context.Users.Remove(user);
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<List<ApplicationUser>> GetAllAsync()
    {
        return await _context.Users
            .Include(u => u.Readings)
            .ToListAsync();
    }

    public async Task<ApplicationUser?> GetByIdAsync(Guid id)
    {
        return await _context.Users
            .Include(u => u.Readings)
            .FirstOrDefaultAsync(u => u.Id == id);
    }

    public async Task<ApplicationUser> UpdateAsync(ApplicationUser user)
    {
        _context.Users.Update(user);
        await _context.SaveChangesAsync();
        return user;
    }
}