using Microsoft.EntityFrameworkCore;
using TurboAzApi.Contexts;
using TurboAzApi.Interface;
using TurboAzApi.Models;

namespace TurboAzApi.Services;

public class IdChecker : IIdChecker
{
    private readonly TurboAzApiContext _context;

    public IdChecker(TurboAzApiContext context)
    {
        _context = context;
    }

    public bool IsUniqueId(int id)
    {
        return _context.Cars.Contains(new Car() { Id = id });
    }
}
