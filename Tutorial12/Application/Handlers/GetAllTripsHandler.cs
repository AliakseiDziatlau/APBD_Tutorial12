using MediatR;
using Tutorial12.Application.DTOs;
using Tutorial12.Application.Queries;
using Tutorial12.Application.Results.GetTripsResults;
using Tutorial12.Domain.Interfaces;
using OneOf;

namespace Tutorial12.Application.Handlers;

public class GetAllTripsHandler : IRequestHandler<GetAllTripsQuery, OneOf<TripsResponseDto, PageNotFound>>
{
    private readonly ITripRepository _tripRepository;

    public GetAllTripsHandler(ITripRepository tripRepository)
    {
        _tripRepository = tripRepository;
    }

    public async Task<OneOf<TripsResponseDto, PageNotFound>> Handle(GetAllTripsQuery request, CancellationToken cancellationToken)
    {
        var totalCount = await _tripRepository.GetTotalTripsCountAsync();
        var totalPages = (int)Math.Ceiling(totalCount / (double)request.PageSize);
        
        if (request.Page > totalPages && totalPages > 0)
        {
            return new PageNotFound();
        }

        var trips = await _tripRepository.GetAllTripsAsync(request.Page, request.PageSize);

        return new TripsResponseDto
        {
            PageNum = request.Page,
            PageSize = request.PageSize,
            AllPages = totalPages,
            Trips = trips.ToList()
        };
    }
}