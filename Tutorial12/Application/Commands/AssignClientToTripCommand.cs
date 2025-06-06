using MediatR;
using Tutorial12.Application.Results.AssignClientToTripResults;
using OneOf;

namespace Tutorial12.Application.Commands;

public class AssignClientToTripCommand : IRequest<OneOf<Success, ClientAlreadyExists, ClientAlreadyRegistered, TripNotFoundOrInPast, TripNameMismatch>>
{
    public int? IdTrip { get; set; }  
    public string TripName { get; set; }
    public string FirstName { get; set; } 
    public string LastName { get; set; } 
    public string Email { get; set; } 
    public string Telephone { get; set; }
    public string Pesel { get; set; }
    public DateTime? PaymentDate { get; set; }
}