using Vehicles.Api.Repositories;
using Vehicles.Api.Services;


var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddTransient<IVehiclesService, VehiclesService>();
builder.Services.AddTransient<IVehiclesRepository, VehiclesRepository>();

var app = builder.Build();
app.UseSwagger();

app.UseSwaggerUI(x => 
{
    x.EnableTryItOutByDefault();
});

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();