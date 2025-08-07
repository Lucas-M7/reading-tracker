using Microsoft.EntityFrameworkCore;
using ReadingTracker.API.Data;
using ReadingTracker.API.Entities;
using ReadingTracker.API.Repositories.Interfaces;

namespace ReadingTracker.API.Repositories;

public class ReadingRepository : IReadingRepository
{
    private readonly AppDbContext _context;

    public ReadingRepository(AppDbContext context)
    {
        _context = context;
    }
    public async Task<Reading> CreateAsync(Reading reading)
    {
        _context.Readings.Add(reading);
        await _context.SaveChangesAsync();
        return reading;
    }

    public async Task<bool> DeleteAsync(Guid id)
    {
        var reading = await _context.Readings.FindAsync(id);
        if (reading == null) return false;

        _context.Readings.Remove(reading);
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<IEnumerable<Reading>> GetAllAsync()
    {
        return await _context.Readings
            .Include(r => r.Book)
            .Include(r => r.User)
            .ToListAsync();
    }

    public async Task<Reading?> GetByIdAsync(Guid id)
    {
        return await _context.Readings
            .Include(r => r.Book)
            .Include(r => r.User)
            .FirstOrDefaultAsync(r => r.ReadingId == id);
    }

    public async Task<Reading> UpdateAsync(Reading reading)
    {
        _context.Readings.Update(reading);
        await _context.SaveChangesAsync();
        return reading;
    }
}