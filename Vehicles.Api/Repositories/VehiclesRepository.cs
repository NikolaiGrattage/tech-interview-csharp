using System.Text.Json;
using System;
//using System.Text.Json.Serialization;
using Vehicles.Api.Models;

namespace Vehicles.Api.Repositories
{
    public class VehiclesRepository : IVehiclesRepository
    {
        List<Vehicle> _vehicles;
        public VehiclesRepository()
        {
            using (StreamReader r = new StreamReader("Repositories/vehicles.json"))
            {
                string json = r.ReadToEnd();
                _vehicles = JsonSerializer.Deserialize<List<Vehicle>>(json) ?? new List<Vehicle>();
            }
        }

        public List<Vehicle> GetAll()
        {
            return _vehicles;
        }

        public List<Vehicle> GetByStringParam(string param, string value)
        {
            List<Vehicle> results;

            switch (param)
            {
                case "make":
                    results
                        = _vehicles.Where(x => x.Make.ToLower() == value.ToLower()).ToList();
                    break;
                case "model":
                    results
                        = _vehicles.Where(x => x.Model.ToLower() == value.ToLower()).ToList();
                    break;
                default:
                    results = new List<Vehicle>();
                    break;
            }

            return results;
        }

        public List<Vehicle> GetRegisteredBetween(DateTime dateFrom, DateTime dateTo)
        {
            var results = _vehicles
                .Where(x => x.DateFirstRegDate.Date >= dateFrom.Date && x.DateFirstRegDate.Date <= dateTo.Date)
                .ToList();

            return results;
        }

        public void AddNew(List<Vehicle> newList)
        {
            string newJson
                = JsonSerializer.Serialize(newList, new JsonSerializerOptions() { WriteIndented = true });

            using (StreamWriter outputFile = new StreamWriter("Repositories/vehicles.json"))
            {
                outputFile.WriteLine(newJson);
            }
        }
    }
}
