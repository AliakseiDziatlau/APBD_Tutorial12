using MediatR;
using Tutorial12.Application.Results;
using OneOf;
using Tutorial12.Application.Results.DeleteClientResults;

namespace Tutorial12.Application.Commands;

public class DeleteClientCommand : IRequest<OneOf<ClientDeleted, ClientNotFound, ClientHasTrips>>
{
    public int Id { get; set; }
}