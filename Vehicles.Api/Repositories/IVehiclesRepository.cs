using Vehicles.Api.Models;

namespace Vehicles.Api.Repositories
{
    public interface IVehiclesRepository
    {
        List<Vehicle> GetAll();

        List<Vehicle> GetByStringParam(string param, string value);

        List<Vehicle> GetRegisteredBetween(DateTime dateFrom, DateTime dateTo);

        void AddNew(List<Vehicle> newList);
    }
}
