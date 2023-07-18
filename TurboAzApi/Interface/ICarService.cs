using TurboAzApi.DTOs;
using TurboAzApi.Models;

namespace TurboAzApi.Interface;

public interface ICarService
{
    public Task<IEnumerable<CarThumbnailResponse>> GetAutos(
        string? searchQuery,
        string? sorting,
        PaginationRequest paginationRequest
    );

    public Task<IEnumerable<CarThumbnailResponse>> LastListing(int count);

    public Task<CarResponse?> GetAuto(int id);

    public Task<int> CreateAuto(CarCreateRequest request);
}
