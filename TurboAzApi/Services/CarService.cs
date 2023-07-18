using Microsoft.EntityFrameworkCore;
using System.Text.Json;
using TurboAzApi.Contexts;
using TurboAzApi.DTOs;
using TurboAzApi.Interface;
using TurboAzApi.Models;

namespace TurboAzApi.Services;

public class CarService : ICarService
{
    private readonly TurboAzApiContext _context;
    private readonly IAzureBlobStorageService _azureBlobStorageService;
    private readonly IIdChecker _idChecker;

    public CarService(
        TurboAzApiContext context,
        IIdChecker idChecker,
        IAzureBlobStorageService azureBlobStorageService
    )
    {
        _context = context;
        _idChecker = idChecker;
        _azureBlobStorageService = azureBlobStorageService;
    }

    public async Task<int> CreateAuto(CarCreateRequest request)
    {
        Car car = new(request) { Id = IdGeneratorService.GenerateUniqueNumericId() };
        car.Owner.Id = IdGeneratorService.GenerateUniqueNumericId();

        var imagesResponse = await _azureBlobStorageService.UploadImagesAsync(
            request.Images,
            car.Updated
        );

        var jsonImages = JsonSerializer.Serialize(imagesResponse);

        car.Images = jsonImages;

        _context.Cars.Add(car);
        await _context.SaveChangesAsync();

        return car.Id;
    }

    public async Task<CarResponse?> GetAuto(int id)
    {
        ArgumentNullException.ThrowIfNull(id, nameof(id));

        Car? car = await _context.Cars
            .Include(car => car.Owner)
            .FirstOrDefaultAsync(c => c.Id == id);

        if (car is not null)
        {
            car.ViewCount++;
            _context.Update(car);
            await _context.SaveChangesAsync();
            return new CarResponse(car);
        }

        return null;
    }

    public async Task<IEnumerable<CarThumbnailResponse>> GetAutos(
        string? searchQuery,
        string? sorting,
        PaginationRequest paginationRequest
    )
    {
        List<Car> query = await _context.Cars.Include(car => car.Owner).ToListAsync();

        if (!string.IsNullOrWhiteSpace(searchQuery))
            query = query.Where(car => car.IsEqualSearchQuery(searchQuery)).ToList();

        if (!string.IsNullOrWhiteSpace(sorting))
            query = sorting switch
            {
                "date" => query.OrderBy(car => car.Updated).ToList(),
                "fromCheapest" => query.OrderBy(car => car.Price).ToList(),
                "fromExpensive" => query.OrderByDescending(car => car.Price).ToList(),
                "mileage" => query.OrderBy(car => car.Mileage).ToList(),
                "productionYear" => query.OrderByDescending(car => car.Year).ToList(),
                _ => query,
            };

        var response = query
            .Skip((paginationRequest.Page - 1) * paginationRequest.PageSize)
            .Take(paginationRequest.PageSize)
            .Select(car => new CarThumbnailResponse(car));

        return response;
    }

    public async Task<IEnumerable<CarThumbnailResponse>> LastListing(int count) =>
        await _context.Cars
            .Include(car => car.Owner)
            .OrderByDescending(c => c.Updated)
            .Take(count)
            .Select(car => new CarThumbnailResponse(car))
            .ToListAsync();
}
