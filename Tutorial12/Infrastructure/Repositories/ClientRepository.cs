using Microsoft.EntityFrameworkCore;
using Tutorial12.Domain.Interfaces;
using Tutorial12.Domain.Models;
using Tutorial12.Infrastructure.Persistence;

namespace Tutorial12.Infrastructure.Repositories;

public class ClientRepository : IClientRepository
{
    private readonly AppDbContext _context;

    public ClientRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task DeleteClientAsync(Client client)
    {
        _context.Clients.Remove(client);
        await _context.SaveChangesAsync();
    }

    public async Task<Client?> GetClientByIdAsync(int id)
    {
        return await _context.Clients
            .Include(c => c.ClientTrips)
            .SingleOrDefaultAsync(c => c.IdClient == id);
    }

    public async Task<Client?> GetClientByPeselAsync(string pesel)
    {
        return await _context.Clients.SingleOrDefaultAsync(c => c.Pesel == pesel);
    }

    public async Task<bool> IsClientRegisteredForTripAsync(int clientId, int tripId)
    {
        return await _context.ClientTrips
            .AnyAsync(ct => ct.IdClient == clientId && ct.IdTrip == tripId);
    }
    
    public async Task AddClientAsync(Client client)
    {
        await _context.Clients.AddAsync(client);
        await _context.SaveChangesAsync();
    }

    public async Task AddClientTripAsync(ClientTrip clientTrip)
    {
        await _context.ClientTrips.AddAsync(clientTrip);
        await _context.SaveChangesAsync();
    }
}