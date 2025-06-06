using MediatR;
using Tutorial12.Application.DTOs;
using Tutorial12.Domain.Models;

namespace Tutorial12.Application.Queries;

public class GetAllTripsQuery : IRequest<TripsResponseDto>
{
    public int Page { get; set; }
    public int PageSize { get; set; }
}