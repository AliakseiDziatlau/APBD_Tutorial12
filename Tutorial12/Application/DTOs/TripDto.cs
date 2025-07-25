namespace Tutorial12.Application.DTOs;

public class TripDto
{
    public string Name { get; set; } = null!;
    public string Description { get; set; } = null!;
    public string DateFrom { get; set; } = null!;
    public string DateTo { get; set; } = null!;
    public int MaxPeople { get; set; }
    public List<CountryDto> Countries { get; set; } = new();
    public List<ClientDto> Clients { get; set; } = new();
}