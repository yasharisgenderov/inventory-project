using Inventory.Core.Entities;
using Inventory.Core.Interfaces;
using Inventory.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Inventory.Infrastructure.Repositories
{
    public class CarRepository : ICarRepository
    {
        private readonly AppDbContext _context;

        public CarRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Car>> GetAllCarsAsync()
        {
            return await _context.Cars.ToListAsync();
        }

        public async Task<Car> GetCarByIdAsync(int id)
        {
            return await _context.Cars.FindAsync(id);
        }

        public async Task AddCarAsync(Car car)
        {
            _context.Cars.Add(car);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateCarAsync(Car car)
        {
            _context.Cars.Update(car);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteCarAsync(int id)
        {
            var car = await _context.Cars.FindAsync(id);
            if (car != null)
            {
                _context.Cars.Remove(car);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<Car>> FetchSortedCarsAsync(string sortBy = "price", string sortOrder = "asc")
        {
            var query = _context.Cars.AsQueryable();

            // Apply sorting logic based on sortBy and sortOrder
            switch (sortBy.ToLower())
            {
                case "brand":
                    query = sortOrder.ToLower() == "asc" ? query.OrderBy(c => c.Brand) : query.OrderByDescending(c => c.Brand);
                    break;
                case "year":
                    query = sortOrder.ToLower() == "asc" ? query.OrderBy(c => c.Year) : query.OrderByDescending(c => c.Year);
                    break;
                case "price":
                    query = sortOrder.ToLower() == "asc" ? query.OrderBy(c => c.Price) : query.OrderByDescending(c => c.Price);
                    break;
                default:
                    query = query.OrderBy(c => c.Price); // Default sorting by price
                    break;
            }

            return await query.Select(c => new Car
            {
                Id = c.Id,
                Brand = c.Brand,
                Model = c.Model,
                Year = c.Year,
                Price = c.Price,
                StockQuantity = c.StockQuantity,
                Condition = c.Condition
            }).ToListAsync();
        }

        public async Task<IEnumerable<Car>> GetLowStockCarsAsync(int threshold)
        {
            return await _context.Cars
                .Where(c => c.StockQuantity <= threshold)
                .ToListAsync();
        }

        // Other methods like Add, Update, GetAll, etc.
    }
}
