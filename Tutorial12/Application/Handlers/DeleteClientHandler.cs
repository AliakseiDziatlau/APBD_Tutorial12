using MediatR;
using Tutorial12.Application.Commands;
using Tutorial12.Application.Results;
using Tutorial12.Domain.Interfaces;
using OneOf;
using Tutorial12.Application.Results.DeleteClientResults;

namespace Tutorial12.Application.Handlers;

public class DeleteClientHandler : IRequestHandler<DeleteClientCommand, OneOf<ClientDeleted, ClientNotFound, ClientHasTrips>>
{
    private readonly IClientRepository _clientRepository;

    public DeleteClientHandler(IClientRepository clientRepository)
    {
        _clientRepository = clientRepository;
    }

    public async Task<OneOf<ClientDeleted, ClientNotFound, ClientHasTrips>> Handle(DeleteClientCommand request, CancellationToken cancellationToken)
    {
        var client = await _clientRepository.GetClientByIdAsync(request.Id);
        
        if (client == null) return new ClientNotFound();

        if (client.ClientTrips.Any()) return new ClientHasTrips();
        
        await _clientRepository.DeleteClientAsync(client);
        
        return new ClientDeleted();
    }
}