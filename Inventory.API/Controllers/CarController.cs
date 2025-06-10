using Inventory.Application.DTOs;
using Inventory.Application.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Inventory.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CarController : ControllerBase
    {
        private readonly ICarService _carService;
        private readonly IStockManagementService _stockManagementService;

        public CarController(ICarService carService, IStockManagementService stockManagementService)
        {
            _carService = carService;
            _stockManagementService = stockManagementService;
        }

        // GET: api/Car
        [HttpGet]
        public async Task<IActionResult> GetCars()
        {
            var cars = await _carService.GetAllCarsAsync();
            return Ok(cars);  // Returns all cars
        }

        // GET: api/Car/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> GetCar(int id)
        {
            var car = await _carService.GetCarByIdAsync(id);
            if (car == null)
            {
                return NotFound();  // Return 404 if the car is not found
            }
            return Ok(car);  // Return the car with the given ID
        }

        // POST: api/Car
        [HttpPost]
        public async Task<IActionResult> AddCar([FromBody] CarDto carDto)
        {
            await _carService.AddCarAsync(carDto);
            return CreatedAtAction(nameof(GetCar), new { id = carDto.Id }, carDto);  // Return 201 Created with the car
        }

        // PUT: api/Car/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCar(int id, [FromBody] CarDto carDto)
        {
            if (id != carDto.Id)
            {
                return BadRequest("Car ID mismatch");  // Return 400 Bad Request if IDs don't match
            }

            await _carService.UpdateCarAsync(carDto);
            return NoContent();  // Return 204 No Content if the update is successful
        }

        // DELETE: api/Car/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCar(int id)
        {
            await _carService.DeleteCarAsync(id);
            return NoContent();  // Return 204 No Content if the deletion is successful
        }

        // GET: api/v1/inventory/cars?sortBy=price&sortOrder=asc
        [HttpGet("sorted-cars")]
        public async Task<IActionResult> GetSortedCars([FromQuery] string sortBy = "price", [FromQuery] string sortOrder = "asc")
        {
            // Call the service method to fetch sorted cars
            var cars = await _carService.GetSortedCarsAsync(sortBy, sortOrder);
            return Ok(cars);
        }

        // Stokdan müəyyən sayda avtomobil çıxarmaq
        [HttpPost("{id}/decrease-stock/{quantity}")]
        public async Task<IActionResult> DecreaseStock(int id, int quantity)
        {
            await _stockManagementService.DecreaseCarStockAsync(id, quantity);
            return NoContent();  // Yenilənmiş stok barədə müvafiq bildiriş göndəriləcək
        }
    }
}
