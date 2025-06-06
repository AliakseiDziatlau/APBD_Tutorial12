using MediatR;
using Tutorial12.Application.DTOs;
using Tutorial12.Application.Results.GetTripsResults;
using OneOf;

namespace Tutorial12.Application.Queries;

public class GetAllTripsQuery : IRequest<OneOf<TripsResponseDto, PageNotFound>>
{
    public int Page { get; set; }
    public int PageSize { get; set; }
}