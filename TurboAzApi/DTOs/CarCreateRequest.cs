using TurboAzApi.Models;

namespace TurboAzApi.DTOs;

public class CarCreateRequest
{
    public string Vendor { get; set; }

    public string Model { get; set; }

    public decimal EngineVolume { get; set; }

    public short Year { get; set; }

    public decimal Price { get; set; }

    public string? Vin { get; set; }

    public Owner Owner { get; set; }

    public string GearBox { get; set; }

    public string Transmission { get; set; }

    public string? MarketAssembled { get; set; }

    public string BanType { get; set; }

    public string Color { get; set; }

    public short HorsePower { get; set; }

    public string Fuel { get; set; }

    public int Mileage { get; set; }

    public string? OwnersCount { get; set; }

    public string? Description { get; set; }

    public List<string>? Equipments { get; set; }

    public List<string> Images { get; set; }
}
