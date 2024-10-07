using Vehicles.Api.Models;

namespace Vehicles.Api.Services
{
    public interface IVehiclesService
    {
        List<Vehicle> GetAll();

        List<Vehicle> GetByMake(string make);

        List<Vehicle> GetByModel(string model);

        List<Vehicle> GetRegisteredBetween(DateTime dateFrom, DateTime dateTo);

        void AddNewVehicle(Vehicle newVehicle);
    }
}
