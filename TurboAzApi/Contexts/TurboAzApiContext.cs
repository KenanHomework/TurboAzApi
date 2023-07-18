using Microsoft.EntityFrameworkCore;
using TurboAzApi.Models;

namespace TurboAzApi.Contexts;

public class TurboAzApiContext : DbContext
{
    public TurboAzApiContext(DbContextOptions<TurboAzApiContext> options)
        : base(options) => Database.EnsureCreated();

    public DbSet<Car> Cars { get; set; }
}
