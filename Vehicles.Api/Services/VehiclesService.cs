using Vehicles.Api.Models;
using Vehicles.Api.Repositories;

namespace Vehicles.Api.Services
{
    public class VehiclesService : IVehiclesService
    {
        private readonly IVehiclesRepository _vehiclesRepository;

        public VehiclesService(IVehiclesRepository vehiclesRepository)
        {
            _vehiclesRepository = vehiclesRepository;
        }

        public List<Vehicle> GetAll()
        {
            var result = _vehiclesRepository.GetAll();
            result = result.OrderBy(x => x.Make).ToList();
            return result;
        }

        public List<Vehicle> GetByMake(string make)
        {
            var result = _vehiclesRepository.GetByStringParam("make", make);
            result = result.OrderBy(x => x.Make).ToList();
            return result;
        }

        public List<Vehicle> GetByModel(string model)
        {
            var result = _vehiclesRepository.GetByStringParam("model", model);
            result = result.OrderBy(x => x.Model).ToList();
            return result;
        }

        public List<Vehicle> GetRegisteredBetween(DateTime dateFrom, DateTime dateTo)
        {
            var result = _vehiclesRepository.GetRegisteredBetween(dateFrom, dateTo);
            result = result.OrderBy(x => x.DateFirstRegDate).ToList();
            return result;
        }

        public void AddNewVehicle(Vehicle newVehicle)
        {
            var existingList = _vehiclesRepository.GetAll();
            existingList.Add(newVehicle);
            _vehiclesRepository.AddNew(existingList);
        }
    }
}
