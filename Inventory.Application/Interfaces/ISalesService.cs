using Inventory.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inventory.Application.Interfaces
{
    public interface ISalesService
    {
        Task<IEnumerable<SaleDto>> GetAllSalesAsync();
        Task<SaleDto> GetSaleByIdAsync(int id);
        Task AddSaleAsync(SaleDto sale);
        Task UpdateSaleAsync(SaleDto sale);
        Task DeleteSaleAsync(int id);

        Task<decimal> GetTotalRevenueByCustomerIdAsync(int customerId);
        Task<string> GetMostPopularCarBrandByCustomerIdAsync(int customerId);
        Task<string> GetMostPopularCarModelByCustomerIdAsync(int customerId);
    }
}
