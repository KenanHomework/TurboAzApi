using Azure.Core;
using System.Text.Json;
using TurboAzApi.Models;

namespace TurboAzApi.DTOs;

public class CarResponse
{
    public CarResponse() { }

    public CarResponse(Car car)
    {
        Id = car.Id;
        Vendor = car.Vendor;
        Model = car.Model;
        EngineVolume = car.EngineVolume;
        Year = car.Year;
        Price = car.Price;
        Vin = car.Vin;
        Owner = car.Owner;
        Updated = car.Updated.ToString("dd/MM/yyyy");
        ViewCount = car.ViewCount;
        GearBox = car.GearBox;
        Transmission = car.Transmission;
        MarketAssembled = car.MarketAssembled;
        BanType = car.BanType;
        Color = car.Color;
        HorsePower = car.HorsePower;
        Fuel = car.Fuel;
        Mileage = car.Mileage;
        OwnersCount = car.OwnersCount;
        Description = car.Description;
        Equipments = car.Equipments?.Split(',').ToList();
        Images = JsonSerializer.Deserialize<List<ImageResponse>>(car.Images)!;
    }

    public CarResponse(Car car, List<ImageResponse> images)
        : this(car)
    {
        Images = images;
    }

    public int Id { get; set; }

    public string Vendor { get; set; }

    public string Model { get; set; }

    public decimal EngineVolume { get; set; }

    public short Year { get; set; }

    public decimal Price { get; set; }

    public string? Vin { get; set; }

    public Owner Owner { get; set; }

    public string Updated { get; set; }

    public int ViewCount { get; set; }

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

    public List<ImageResponse> Images { get; set; }
}
