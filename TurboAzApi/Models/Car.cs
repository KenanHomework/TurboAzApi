using System.Linq;
using TurboAzApi.DTOs;
using TurboAzApi.Interface;

namespace TurboAzApi.Models;

public class Car : IConvertibleSearchQuery, ISearchQueryChecker
{
    public Car() { }

    public Car(CarCreateRequest request)
    {
        Vendor = request.Vendor;
        Model = request.Model;
        EngineVolume = request.EngineVolume;
        Year = request.Year;
        Price = request.Price;
        Vin = request.Vin;
        Owner = request.Owner;
        Updated = DateTime.Now;
        ViewCount = 0;
        GearBox = request.GearBox;
        Transmission = request.Transmission;
        MarketAssembled = request.MarketAssembled;
        BanType = request.BanType;
        Color = request.Color;
        HorsePower = request.HorsePower;
        Fuel = request.Fuel;
        Mileage = request.Mileage;
        OwnersCount = request.OwnersCount;
        Description = request.Description;
        Equipments = string.Join(',', request.Equipments ?? Enumerable.Empty<string>());
        Images = string.Join(',', request.Images ?? Enumerable.Empty<string>());
    }

    public int Id { get; set; }

    public string Vendor { get; set; }

    public string Model { get; set; }

    public decimal EngineVolume { get; set; }

    public short Year { get; set; }

    public decimal Price { get; set; }

    public string? Vin { get; set; }

    public Owner Owner { get; set; }

    public DateTime Updated { get; set; } = DateTime.Now;

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

    public string? Equipments { get; set; }

    public string Images { get; set; }

    public string GetSearchQuery() => $"{Vendor} {Model}";

    public bool IsEqualSearchQuery(string searchQuery) =>
        GetSearchQuery().Contains(searchQuery.ToUpper())
        || new Simila.Core.Simila() { Treshold = 0.2f }.AreSimilar(GetSearchQuery(), searchQuery);
}
