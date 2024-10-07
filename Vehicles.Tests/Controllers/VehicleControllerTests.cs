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
using Vehicles.Api.Services;

namespace Vehicles.Tests.Controllers
{
    public class VehicleControllerTests
    {
        private VehiclesController _vehiclesController;
        private Mock<IVehiclesService> _vehicleServiceMock;
        private Mock<ILogger<VehiclesController>> _loggerMock;

        [SetUp]
        public void Setup()
        {
            _vehicleServiceMock = new Mock<IVehiclesService>();
            _loggerMock = new Mock<ILogger<VehiclesController>>();

            _vehiclesController
                = new VehiclesController(_vehicleServiceMock.Object, _loggerMock.Object);
        }

        [TearDown]
        public void TearDown()
        {
            _vehicleServiceMock.Reset();
            _loggerMock.Reset();
        }

        #region List()

        [Test]
        public void List_ReturnsOK()
        {
            // Arrange
            var resultObj = new List<Vehicle> { new Vehicle() };
            var expected = new OkObjectResult(resultObj);

            _vehicleServiceMock.Setup(x => x.GetAll()).Returns(resultObj);

            // Act
            var result = (OkObjectResult)_vehiclesController.List();

            var expectedJson = JsonSerializer.Serialize(expected);
            var resultJson = JsonSerializer.Serialize(result);

            // Assert
            Assert.IsNotNull(result);
            Assert.That(resultJson, Is.EqualTo(expectedJson));
        }

        [Test]
        public void List_ReturnsProblem()
        {
            // Arrange
            var problemDetails = new ProblemDetails();
            problemDetails.Status = 500;
            problemDetails.Detail = "Exception of type 'System.Exception' was thrown.";
            var expected = new ObjectResult(problemDetails);
            expected.StatusCode = 500;

            _vehicleServiceMock.Setup(x => x.GetAll()).Throws(new Exception());

            // Act
            var result = (ObjectResult)_vehiclesController.List();

            var expectedJson = JsonSerializer.Serialize(expected);
            var resultJson = JsonSerializer.Serialize(result);

            // Assert
            Assert.IsNotNull(result);
            Assert.That(resultJson, Is.EqualTo(expectedJson));
        }

        [Test]
        public void List_ReturnsNotFound()
        {
            // Arrange
            var expected = new NotFoundResult();

            _vehicleServiceMock.Setup(x => x.GetAll()).Returns((List<Vehicle>)null);

            // Act
            var result = (NotFoundResult)_vehiclesController.List();

            var expectedJson = JsonSerializer.Serialize(expected);
            var resultJson = JsonSerializer.Serialize(result);

            // Assert
            Assert.IsNotNull(result);
            Assert.That(resultJson, Is.EqualTo(expectedJson));
        }

        #endregion

        #region ListByMake()

        [Test]
        public void ListByMake_ReturnsOK()
        {
            // Arrange
            var resultObj = new List<Vehicle> { new Vehicle() };
            var expected = new OkObjectResult(resultObj);

            _vehicleServiceMock
                .Setup(x => x.GetByMake(It.IsAny<string>())).Returns(resultObj);

            // Act
            var result
                = (OkObjectResult)_vehiclesController.ListByMake(It.IsAny<string>());

            var expectedJson = JsonSerializer.Serialize(expected);
            var resultJson = JsonSerializer.Serialize(result);

            // Assert
            Assert.IsNotNull(result);
            Assert.That(resultJson, Is.EqualTo(expectedJson));
        }

        [Test]
        public void ListByMake_ReturnsProblem()
        {
            // Arrange
            var problemDetails = new ProblemDetails();
            problemDetails.Status = 500;
            problemDetails.Detail = "Exception of type 'System.Exception' was thrown.";
            var expected = new ObjectResult(problemDetails);
            expected.StatusCode = 500;

            _vehicleServiceMock
                .Setup(x => x.GetByMake(It.IsAny<string>())).Throws(new Exception());

            // Act
            var result
                = (ObjectResult)_vehiclesController.ListByMake(It.IsAny<string>());

            var expectedJson = JsonSerializer.Serialize(expected);
            var resultJson = JsonSerializer.Serialize(result);

            // Assert
            Assert.IsNotNull(result);
            Assert.That(resultJson, Is.EqualTo(expectedJson));
        }

        [Test]
        public void ListByMake_ReturnsNotFound()
        {
            // Arrange
            var expected = new NotFoundResult();

            _vehicleServiceMock
                .Setup(x => x.GetByMake(It.IsAny<string>())).Returns((List<Vehicle>)null);

            // Act
            var result
                = (NotFoundResult)_vehiclesController.ListByMake(It.IsAny<string>());

            var expectedJson = JsonSerializer.Serialize(expected);
            var resultJson = JsonSerializer.Serialize(result);

            // Assert
            Assert.IsNotNull(result);
            Assert.That(resultJson, Is.EqualTo(expectedJson));
        }

        #endregion

        #region ListByModel()

        // TODO

        #endregion

        #region ListRegisteredBetween()

        // TODO

        #endregion

        #region AddNewVehicle()

        [Test]
        public void AddNewVehicle_ReturnsNoContent()
        {
            // Arrange
            var requestObj = new Vehicle();
            var expected = new NoContentResult();

            _vehicleServiceMock.Setup(x => x.AddNewVehicle(requestObj));

            // Act
            var result
                = (NoContentResult)_vehiclesController.AddNewVehicle(requestObj);

            var expectedJson = JsonSerializer.Serialize(expected);
            var resultJson = JsonSerializer.Serialize(result);

            // Assert
            Assert.IsNotNull(result);
            Assert.That(resultJson, Is.EqualTo(expectedJson));
        }

        [Test]
        [TestCase("Ford", "Mustang")]
        [TestCase("BMW", "M3")]
        public void AddNewVehicle_ReturnsProblem(string make, string model)
        {
            // Arrange
            var requestObj = new Vehicle();
            requestObj.Make = make;
            requestObj.Model = model;

            var problemDetails = new ProblemDetails();
            problemDetails.Status = 500;
            problemDetails.Detail
                = $"Error adding new vehicle: {make} - {model}. Exception of type 'System.Exception' was thrown.";
            var expected = new ObjectResult(problemDetails);
            expected.StatusCode = 500;

            _vehicleServiceMock
                .Setup(x => x.AddNewVehicle(requestObj)).Throws(new Exception());

            // Act
            var result = (ObjectResult)_vehiclesController.AddNewVehicle(requestObj);

            var expectedJson = JsonSerializer.Serialize(expected);
            var resultJson = JsonSerializer.Serialize(result);

            // Assert
            Assert.IsNotNull(result);
            Assert.That(resultJson, Is.EqualTo(expectedJson));
        }

        #endregion
    }
}
