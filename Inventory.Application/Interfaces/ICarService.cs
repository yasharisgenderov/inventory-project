using Inventory.Application.DTOs;
using Inventory.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inventory.Application.Interfaces
{
    public interface ICarService
    {
        Task<IEnumerable<CarDto>> GetAllCarsAsync();
        Task<CarDto> GetCarByIdAsync(int id);
        Task AddCarAsync(CarDto car);
        Task UpdateCarAsync(CarDto car);
        Task DeleteCarAsync(int id);
        Task<IEnumerable<CarDto>> GetSortedCarsAsync(string sortBy = "price", string sortOrder = "asc");
    }
}
