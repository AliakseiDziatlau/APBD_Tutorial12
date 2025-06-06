using Tutorial12.Application.DTOs;
using Tutorial12.Domain.Models;

namespace Tutorial12.Domain.Interfaces;

public interface ITripRepository
{
    Task<ICollection<TripDto>> GetAllTripsAsync(int page, int pageSize);
    Task<int> GetTotalTripsCountAsync();
    Task<Trip?> GetTripByIdAsync(int tripId);
}