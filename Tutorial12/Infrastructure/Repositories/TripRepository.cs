using Microsoft.EntityFrameworkCore;
using Tutorial12.Application.DTOs;
using Tutorial12.Domain.Interfaces;
using Tutorial12.Domain.Models;
using Tutorial12.Infrastructure.Persistence;

namespace Tutorial12.Infrastructure.Repositories;

public class TripRepository : ITripRepository
{
    private readonly AppDbContext _context;

    public TripRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<ICollection<TripDto>> GetAllTripsAsync(int page, int pageSize)
    {
        return await _context.Trips
            .Include(t => t.IdCountries)
            .Include(t => t.ClientTrips)
            .ThenInclude(ct => ct.IdClientNavigation)
            .OrderByDescending(t => t.DateFrom)
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .Select(t => new TripDto
            {
                Name = t.Name,
                Description = t.Description,
                DateFrom = t.DateFrom.ToString("yyyy-MM-dd"),
                DateTo = t.DateTo.ToString("yyyy-MM-dd"),
                MaxPeople = t.MaxPeople,
                Countries = t.IdCountries.Select(c => new CountryDto
                {
                    Name = c.Name
                }).ToList(),
                Clients = t.ClientTrips.Select(ct => new ClientDto
                {
                    FirstName = ct.IdClientNavigation.FirstName,
                    LastName = ct.IdClientNavigation.LastName
                }).ToList()
            })
            .ToListAsync();
    }
    
    public async Task<int> GetTotalTripsCountAsync()
    {
        return await _context.Trips.CountAsync();
    }
    
    public async Task<Trip?> GetTripByIdAsync(int tripId)
    {
        return await _context.Trips
            .Include(t => t.IdCountries)
            .Include(t => t.ClientTrips) 
            .FirstOrDefaultAsync(t => t.IdTrip == tripId);
    }
}