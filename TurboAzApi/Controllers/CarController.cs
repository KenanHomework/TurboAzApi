using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using TurboAzApi.DTOs;
using TurboAzApi.Interface;
using TurboAzApi.Models;

namespace TurboAzApi.Controllers;

[Route("api/")]
[ApiController]
public class CarController : ControllerBase
{
    private readonly ICarService _carService;
    private readonly IAzureBlobStorageService _azureBlobStorageService;

    public CarController(ICarService carService, IAzureBlobStorageService azureBlobStorageService)
    {
        _carService = carService;
        _azureBlobStorageService = azureBlobStorageService;
    }

    [HttpPost]
    public async Task<ActionResult<int>> CreateAuto(CarCreateRequest request)
    {
        try
        {
            int resultId = await _carService.CreateAuto(request);
            await Console.Out.WriteLineAsync($"ResultId: {resultId}");
            return Ok(resultId);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<CarResponse?>> GetAuto(int id)
    {
        try
        {
            CarResponse? car = await _carService.GetAuto(id);

            if (car is null)
                return NotFound();

            return Ok(car);
        }
        catch (Exception)
        {
            return NotFound();
        }
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<CarThumbnailResponse>>> GetAutos(
        [FromQuery] string? searchQuery,
        [FromQuery] string? sorting,
        [FromQuery] PaginationRequest paginationRequest
    )
    {
        try
        {
            var result = await _carService.GetAutos(searchQuery, sorting, paginationRequest);

            return Ok(result);
        }
        catch (Exception)
        {
            return NotFound();
        }
    }

    [HttpGet("lastListings")]
    public async Task<ActionResult<IEnumerable<CarThumbnailResponse>>> LastListing(int count)
    {
        try
        {
            var result = await _carService.LastListing(count);

            return Ok(result);
        }
        catch (Exception)
        {
            return NotFound();
        }
    }
}
