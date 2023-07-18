using System.Text.Json;
using TurboAzApi.Models;

namespace TurboAzApi.DTOs
{
    public class CarThumbnailResponse
    {
        public CarThumbnailResponse() { }

        public CarThumbnailResponse(Car car)
        {
            Id = car.Id;
            Price = car.Price;
            Title = $"{car.Vendor} {car.Model}";
            Year = car.Year;
            EngineVolume = car.EngineVolume;
            Mileage = car.Mileage;
            City = car.Owner.City;
            Updated = car.Updated.ToString("dd.MM.yyyy hh:mm");
            Image = JsonSerializer
                .Deserialize<List<ImageResponse>>(car.Images)!
                .FirstOrDefault()!
                .Original;
            HasVin = !string.IsNullOrWhiteSpace(car.Vin);
        }

        public int Id { get; set; }

        public bool HasVin { get; set; }

        public decimal Price { get; set; }

        public string Title { get; set; } = string.Empty;

        public short Year { get; set; }

        public decimal EngineVolume { get; set; }

        public int Mileage { get; set; }

        public string City { get; set; } = string.Empty;

        public string Updated { get; set; }

        public string Image { get; set; }
    }
}
