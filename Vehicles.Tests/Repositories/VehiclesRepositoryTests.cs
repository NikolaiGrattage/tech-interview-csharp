using Moq;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Vehicles.Api.Models;
using Vehicles.Api.Repositories;
using Vehicles.Api.Services;

namespace Vehicles.Tests.Repositories
{
    public class VehiclesRepositoryTests
    {
        
        private IVehiclesRepository _vehiclesRepository;

        [SetUp]
        public void Setup()
        {      
            _vehiclesRepository = new VehiclesRepository();
        } 

        [TearDown]
        public void TearDown()
        {
        }

        [TestCaseSource(typeof(VehiclesTestData))]
        [Test]
        public void GetAll(List<Vehicle> testData)
        {          
            // Act
            var result = _vehiclesRepository.GetAll();

            var expectedJson = JsonSerializer.Serialize(testData);
            var resultJson = JsonSerializer.Serialize(result);

            // Assert
            Assert.IsNotNull(result);
            Assert.That(resultJson, Is.EqualTo(expectedJson));
        }

        [TestCaseSource(typeof(VehiclesTestData))]
        [Test]
        public void GetByStringParam_Make(List<Vehicle> testData)
        {
            // Arrange
            var makes = new List<string>
            {
                "BMW",
                "Ford",
                "Renault"
            };

            foreach (var make in makes) 
            {
                var expected
                    = testData.Where(x => x.Make.ToLower() == make.ToLower()).ToList();

                // Act
                var result = _vehiclesRepository.GetByStringParam("make", make);

                var expectedJson = JsonSerializer.Serialize(expected);
                var resultJson = JsonSerializer.Serialize(result);

                // Assert
                Assert.IsNotNull(result);
                Assert.That(resultJson, Is.EqualTo(expectedJson));
            }               
        }

        [TestCaseSource(typeof(VehiclesTestData))]
        [Test]
        public void GetByStringParam_Model(List<Vehicle> testData)
        {
            // Arrange
            var models = new List<string>
            {
                "Fiesta",
                "Corsa",
                "A1"
            };

            foreach (var model in models)
            {
                var expected
                    = testData.Where(x => x.Model.ToLower() == model.ToLower()).ToList();

                // Act
                var result = _vehiclesRepository.GetByStringParam("model", model);

                var expectedJson = JsonSerializer.Serialize(expected);
                var resultJson = JsonSerializer.Serialize(result);

                // Assert
                Assert.IsNotNull(result);
                Assert.That(resultJson, Is.EqualTo(expectedJson));
            }
        }
    }

    public class VehiclesTestData : IEnumerable
    {
        private List<Vehicle> _testVehicles;

        public VehiclesTestData()
        {
            using (StreamReader r = new StreamReader("Repositories/vehicles.json"))
            {
                string json = r.ReadToEnd();
                _testVehicles = JsonSerializer.Deserialize<List<Vehicle>>(json) ?? new List<Vehicle>();
            }
        }

        public IEnumerator GetEnumerator()
        {
            yield return _testVehicles;
        }
    }
}
