using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Net;
using Vehicles.Api.Models;
using Vehicles.Api.Models.Api;
using Vehicles.Api.Services;

namespace Vehicles.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class VehiclesController : ControllerBase
    {
        private readonly IVehiclesService _vehiclesService;
        private readonly ILogger<VehiclesController> _logger;

        public VehiclesController(IVehiclesService vehiclesService,
            ILogger<VehiclesController> logger)
        {
            _vehiclesService = vehiclesService;
            _logger = logger;
        }

        [HttpGet]
        [Route("list")]
        public IActionResult List()
        {
            try
            {
                var result = _vehiclesService.GetAll();

                if (result == null)
                {
                    return NotFound();
                }

                return Ok(result);
            }
            catch (Exception e)
            {
                _logger.LogError($"Error listing vehicles: {e.Message}");
                return Problem(e.Message);
            }
        }

        [HttpGet]
        [Route("list/make/{make}")]
        public IActionResult ListByMake(string make) 
        {
            try
            {
                var result = _vehiclesService.GetByMake(make);

                if (result == null)
                {
                    return NotFound();
                }

                return Ok(result);
            }
            catch (Exception e)
            {
                _logger.LogError($"Error listing vehicles for {make}: {e.Message}");
                return Problem(e.Message);
            }
        }

        [HttpGet]
        [Route("list/model/{model}")]
        public IActionResult ListByModel(string model)
        {
            try
            {
                var result = _vehiclesService.GetByModel(model);

                if (result == null)
                {
                    return NotFound();
                }

                return Ok(result);
            }
            catch (Exception e)
            {
                _logger.LogError($"Error listing vehicles for {model}: {e.Message}");
                return Problem(e.Message);
            }
        }

        [HttpPost]
        [Route("list/registered")]
        public IActionResult ListRegisteredBetween([FromBody]RegisteredRequest request)
        {
            try
            {
                var result = _vehiclesService.GetRegisteredBetween(request.DateFrom, request.DateTo);

                if (result == null)
                {
                    return NotFound();
                }

                return Ok(result);
            }
            catch (Exception e)
            {
                _logger.LogError(
                    $"Error listing vehicles for range {request.DateFrom} - {request.DateTo}: {e.Message}");
                return Problem(e.Message);
            }
        }

        [HttpPost]
        [Route("Add")]
        public IActionResult AddNewVehicle([FromBody] Vehicle vehicleRequest)
        {
            try
            {
                _vehiclesService.AddNewVehicle(vehicleRequest);

                return NoContent();
            }
            catch (Exception e)
            {
                _logger.LogError(
                    $"Error adding new vehicle: {e.Message}");
                return Problem($"Error adding new vehicle: {vehicleRequest.Make} - {vehicleRequest.Model}. {e.Message}");
            }
        }
    }
}