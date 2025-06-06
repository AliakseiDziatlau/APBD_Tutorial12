using MediatR;
using Tutorial12.Application.Commands;
using Tutorial12.Application.Results.AssignClientToTripResults;
using Tutorial12.Domain.Interfaces;
using OneOf;
using Tutorial12.Domain.Models;

namespace Tutorial12.Application.Handlers;

public class AssignClientToTripHandler : IRequestHandler<AssignClientToTripCommand, OneOf<Success, ClientAlreadyExists, ClientAlreadyRegistered, TripNotFoundOrInPast, TripNameMismatch>>
{
    private readonly IClientRepository _clientRepository;
    private readonly ITripRepository _tripRepository;

    public AssignClientToTripHandler(IClientRepository clientRepository, ITripRepository tripRepository)
    {
        _clientRepository = clientRepository;
        _tripRepository = tripRepository;
    }

    public async Task<OneOf<Success, ClientAlreadyExists, ClientAlreadyRegistered, TripNotFoundOrInPast, TripNameMismatch>> Handle(AssignClientToTripCommand request, CancellationToken cancellationToken)
    {
        var trip = await _tripRepository.GetTripByIdAsync(request.IdTrip!.Value);
        if (trip == null || trip.DateFrom <= DateTime.UtcNow)
            return new TripNotFoundOrInPast();
        
        if (!string.Equals(trip.Name.Trim(), request.TripName.Trim(), StringComparison.OrdinalIgnoreCase))
            return new TripNameMismatch();

        var client = await _clientRepository.GetClientByPeselAsync(request.Pesel);
        if (client != null)
        {
            var alreadyRegistered = await _clientRepository.IsClientRegisteredForTripAsync(client.IdClient, request.IdTrip!.Value);
            if (alreadyRegistered)
                return new ClientAlreadyRegistered();

            return new ClientAlreadyExists(); 
        }
        
        var newClient = new Client
        {
            FirstName = request.FirstName,
            LastName = request.LastName,
            Email = request.Email,
            Telephone = request.Telephone,
            Pesel = request.Pesel
        };

        await _clientRepository.AddClientAsync(newClient);
        
        var clientTrip = new ClientTrip
        {
            IdClient = newClient.IdClient,
            IdTrip = request.IdTrip!.Value,
            RegisteredAt = DateTime.UtcNow,
            PaymentDate = request.PaymentDate
        };

        await _clientRepository.AddClientTripAsync(clientTrip);

        return new Success();
    }
}