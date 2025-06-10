using Inventory.Application.DTOs;
using Inventory.Application.Interfaces;
using Inventory.Core.Entities;
using Inventory.Core.Interfaces;

namespace Inventory.Application.Services
{
    public class SalesService : ISalesService
    {
        private readonly ISaleRepository _saleRepository;
        private readonly ICarRepository _carRepository;

        public SalesService(ISaleRepository saleRepository, ICarRepository carRepository)
        {
            _saleRepository = saleRepository;
            _carRepository = carRepository;
        }

        public async Task<IEnumerable<SaleDto>> GetAllSalesAsync()
        {
            var sales = await _saleRepository.GetAllSalesAsync();
            return sales.Select(s => new SaleDto
            {
                Id = s.Id,
                CarId = s.CarId,
                CustomerId = s.CustomerId,
                SaleDate = s.SaleDate,
                SalePrice = s.SalePrice
            });
        }

        public async Task<SaleDto> GetSaleByIdAsync(int id)
        {
            var sale = await _saleRepository.GetSaleByIdAsync(id);
            if (sale == null) return null;

            return new SaleDto
            {
                Id = sale.Id,
                CarId = sale.CarId,
                CustomerId = sale.CustomerId,
                SaleDate = sale.SaleDate,
                SalePrice = sale.SalePrice
            };
        }

        public async Task AddSaleAsync(SaleDto saleDto)
        {
            var sale = new Sale
            {
                CarId = saleDto.CarId,
                CustomerId = saleDto.CustomerId,
                SaleDate = saleDto.SaleDate,
                SalePrice = saleDto.SalePrice
            };

            await _saleRepository.AddSaleAsync(sale);
        }

        public async Task UpdateSaleAsync(SaleDto saleDto)
        {
            var sale = await _saleRepository.GetSaleByIdAsync(saleDto.Id);
            if (sale == null) return;

            sale.CarId = saleDto.CarId;
            sale.CustomerId = saleDto.CustomerId;
            sale.SaleDate = saleDto.SaleDate;
            sale.SalePrice = saleDto.SalePrice;

            await _saleRepository.UpdateSaleAsync(sale);
        }

        public async Task DeleteSaleAsync(int id)
        {
            await _saleRepository.DeleteSaleAsync(id);
        }

        public async Task<decimal> GetTotalRevenueByCustomerIdAsync(int customerId)
        {
            var sales = await _saleRepository.GetSalesByCustomerIdAsync(customerId);
            return sales.Sum(s => s.SalePrice);  // Satış qiymətləri cəmi
        }

        public async Task<string> GetMostPopularCarBrandByCustomerIdAsync(int customerId)
        {
            var sales = await _saleRepository.GetSalesByCustomerIdAsync(customerId);
            var mostPopularBrand = sales
                .GroupBy(s => s.Car.Brand)
                .OrderByDescending(g => g.Count())
                .FirstOrDefault()?.Key;

            return mostPopularBrand ?? "No data";  // Ən çox satılan marka
        }

        public async Task<string> GetMostPopularCarModelByCustomerIdAsync(int customerId)
        {
            var sales = await _saleRepository.GetSalesByCustomerIdAsync(customerId);
            var mostPopularModel = sales
                .GroupBy(s => s.Car.Model)
                .OrderByDescending(g => g.Count())
                .FirstOrDefault()?.Key;

            return mostPopularModel ?? "No data";  // Ən çox satılan model
        }
    }
}
