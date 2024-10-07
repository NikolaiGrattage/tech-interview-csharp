using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Vehicles.Api.Controllers;
using Vehicles.Api.Models;
using Vehicles.Api.Repositories;
using Vehicles.Api.Services;

namespace Vehicles.Tests.Services
{
    public class VehiclesServiceTests
    {
        private IVehiclesService _vehicleService;
        private Mock<IVehiclesRepository> _vehicleRepositoryMock;

        [SetUp]
        public void Setup()
        {
            _vehicleRepositoryMock = new Mock<IVehiclesRepository>();
            _vehicleService = new VehiclesService(_vehicleRepositoryMock.Object);
        }


        [TearDown]
        public void TearDown()
        {
            _vehicleRepositoryMock.Reset();
        }

        [Test]
        public void GetAll()
        {
            // Arrange
            var expected = new List<Vehicle> { new Vehicle() };

            _vehicleRepositoryMock.Setup(x => x.GetAll()).Returns(expected);

            // Act
            var result = _vehicleService.GetAll();

            var expectedJson = JsonSerializer.Serialize(expected);
            var resultJson = JsonSerializer.Serialize(result);

            // Assert
            Assert.IsNotNull(result);
            Assert.That(resultJson, Is.EqualTo(expectedJson));
        }

        [Test]
        public void GetByMake()
        {
            // Arrange
            var expected = new List<Vehicle> { new Vehicle() };

            _vehicleRepositoryMock
                .Setup(x => x.GetByStringParam(It.IsAny<string>(), It.IsAny<string>()))
                .Returns(expected);

            // Act
            var result = _vehicleService.GetByMake(It.IsAny<string>());

            var expectedJson = JsonSerializer.Serialize(expected);
            var resultJson = JsonSerializer.Serialize(result);

            // Assert
            Assert.IsNotNull(result);
            Assert.That(resultJson, Is.EqualTo(expectedJson));
        }

        [Test]
        public void GetByModel()
        {
            // Arrange
            var expected = new List<Vehicle> { new Vehicle() };

            _vehicleRepositoryMock
                .Setup(x => x.GetByStringParam(It.IsAny<string>(), It.IsAny<string>()))
                .Returns(expected);

            // Act
            var result = _vehicleService.GetByModel(It.IsAny<string>());

            var expectedJson = JsonSerializer.Serialize(expected);
            var resultJson = JsonSerializer.Serialize(result);

            // Assert
            Assert.IsNotNull(result);
            Assert.That(resultJson, Is.EqualTo(expectedJson));
        }

        [Test]
        public void GetRegisteredBetween()
        {
            // Arrange
            var expected = new List<Vehicle> { new Vehicle() };

            _vehicleRepositoryMock
                .Setup(x => x.GetRegisteredBetween(It.IsAny<DateTime>(), It.IsAny<DateTime>()))
                .Returns(expected);

            // Act
            var result
                = _vehicleService.GetRegisteredBetween(It.IsAny<DateTime>(), It.IsAny<DateTime>());

            var expectedJson = JsonSerializer.Serialize(expected);
            var resultJson = JsonSerializer.Serialize(result);

            // Assert
            Assert.IsNotNull(result);
            Assert.That(resultJson, Is.EqualTo(expectedJson));
        }

        [Test]
        public void AddNewVehicle()
        {
            // Arrange
            var requestObj = new Vehicle();
            var addModel = new List<Vehicle> { requestObj };

            _vehicleRepositoryMock.Setup(x => x.GetAll()).Returns(addModel);

            _vehicleRepositoryMock.Setup(x => x.AddNew(addModel));

            // Act
            _vehicleService.AddNewVehicle(requestObj);

            // Assert
            _vehicleRepositoryMock.Verify(x => x.GetAll(), Times.Once);
            _vehicleRepositoryMock.Verify(x => x.AddNew(addModel), Times.Once);
        }
    }
}
