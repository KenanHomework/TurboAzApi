using Microsoft.EntityFrameworkCore;
using TurboAzApi.Configurations;
using TurboAzApi.Contexts;
using TurboAzApi.Interface;
using TurboAzApi.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<TurboAzApiContext>(
    options => options.UseSqlite("Data Source=TurboAzDB")
);

// Adding Azure Configuration
AzureConfig azureConfig = new();
builder.Configuration.GetSection("AzureConfig").Bind(azureConfig);
builder.Services.AddSingleton(azureConfig);

//Adding Services
builder.Services.AddScoped<IAzureBlobStorageService, AzureBlobStorageService>();
builder.Services.AddScoped<IIdChecker, IdChecker>();
builder.Services.AddScoped<ICarService, CarService>();

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddCors(
    p =>
        p.AddPolicy(
            "corsapp",
            builder =>
            {
                builder.WithOrigins("http://localhost:5173").AllowAnyMethod().AllowAnyHeader();
            }
        )
);

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors("corsapp");

app.UseAuthorization();

app.MapControllers();

app.Run();
