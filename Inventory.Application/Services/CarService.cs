using Inventory.Application.DTOs;
using Inventory.Application.Interfaces;
using Inventory.Core.Entities;
using Inventory.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inventory.Application.Services
{
    public class CarService : ICarService
    {
        private readonly ICarRepository _carRepository;

        public CarService(ICarRepository carRepository)
        {
            _carRepository = carRepository;
        }

        public async Task<IEnumerable<CarDto>> GetAllCarsAsync()
        {
            var cars = await _carRepository.GetAllCarsAsync();
            return cars.Select(c => new CarDto
            {
                Id = c.Id,
                Brand = c.Brand,
                Model = c.Model,
                Year = c.Year,
                Price = c.Price,
                StockQuantity = c.StockQuantity,
                Condition = c.Condition
            });
        }

        public async Task<CarDto> GetCarByIdAsync(int id)
        {
            var car = await _carRepository.GetCarByIdAsync(id);
            if (car == null) return null;

            return new CarDto
            {
                Id = car.Id,
                Brand = car.Brand,
                Model = car.Model,
                Year = car.Year,
                Price = car.Price,
                StockQuantity = car.StockQuantity,
                Condition = car.Condition
            };
        }

        public async Task AddCarAsync(CarDto carDto)
        {
            var car = new Car
            {
                Brand = carDto.Brand,
                Model = carDto.Model,
                Year = carDto.Year,
                Price = carDto.Price,
                StockQuantity = carDto.StockQuantity,
                Condition = carDto.Condition
            };

            await _carRepository.AddCarAsync(car);
        }

        public async Task UpdateCarAsync(CarDto carDto)
        {
            var car = await _carRepository.GetCarByIdAsync(carDto.Id);
            if (car == null) return;

            car.Brand = carDto.Brand;
            car.Model = carDto.Model;
            car.Year = carDto.Year;
            car.Price = carDto.Price;
            car.StockQuantity = carDto.StockQuantity;
            car.Condition = carDto.Condition;

            await _carRepository.UpdateCarAsync(car);
        }

        public async Task DeleteCarAsync(int id)
        {
            await _carRepository.DeleteCarAsync(id);
        }
    }
}
