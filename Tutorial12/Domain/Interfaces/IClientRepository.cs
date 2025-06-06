using Tutorial12.Domain.Models;

namespace Tutorial12.Domain.Interfaces;

public interface IClientRepository
{
    Task DeleteClientAsync(Client client);
    Task<Client?> GetClientByIdAsync(int id);
    Task<Client?> GetClientByPeselAsync(string pesel);
    Task<bool> IsClientRegisteredForTripAsync(int clientId, int tripId);
    Task AddClientAsync(Client client);
    Task AddClientTripAsync(ClientTrip clientTrip);
}