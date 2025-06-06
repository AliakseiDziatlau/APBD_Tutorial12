using MediatR;
using Tutorial12.Application.DTOs;
using Tutorial12.Application.Queries;
using Tutorial12.Domain.Interfaces;

namespace Tutorial12.Application.Handlers;

public class GetAllTripsHandler : IRequestHandler<GetAllTripsQuery, TripsResponseDto>
{
    private readonly ITripRepository _tripRepository;

    public GetAllTripsHandler(ITripRepository tripRepository)
    {
        _tripRepository = tripRepository;
    }

    public async Task<TripsResponseDto> Handle(GetAllTripsQuery request, CancellationToken cancellationToken)
    {
        var totalCount = await _tripRepository.GetTotalTripsCountAsync();
        var totalPages = (int)Math.Ceiling(totalCount / (double)request.PageSize);

        var trips = (await _tripRepository.GetAllTripsAsync(request.Page, request.PageSize)).ToList();

        return new TripsResponseDto
        {
            PageNum = request.Page,
            PageSize = request.PageSize,
            AllPages = totalPages,
            Trips = trips
        };
    }
}