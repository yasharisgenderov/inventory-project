using Inventory.Core.Entities;
using Inventory.Core.Interfaces;
using Inventory.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inventory.Infrastructure.Repositories
{
    public class SaleRepository : ISaleRepository
    {
        private readonly AppDbContext _context;

        public SaleRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Sale>> GetAllSalesAsync()
        {
            return await _context.Sales.ToListAsync();
        }

        public async Task<Sale> GetSaleByIdAsync(int id)
        {
            return await _context.Sales.FindAsync(id);
        }

        public async Task AddSaleAsync(Sale sale)
        {
            _context.Sales.Add(sale);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateSaleAsync(Sale sale)
        {
            _context.Sales.Update(sale);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteSaleAsync(int id)
        {
            var sale = await _context.Sales.FindAsync(id);
            if (sale != null)
            {
                _context.Sales.Remove(sale);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<Sale>> GetSalesByCustomerIdAsync(int customerId)
        {
            return await _context.Sales
                .Where(s => s.CustomerId == customerId)
                .Include(s => s.Car)
                .Include(s => s.Customer)
                .ToListAsync();
        }

        public async Task<IEnumerable<Sale>> GetSalesByDateRangeAsync(DateTime startDate, DateTime endDate)
        {
            return await _context.Sales
                .Where(s => s.SaleDate >= startDate && s.SaleDate <= endDate)
                .Include(s => s.Car)
                .Include(s => s.Customer)
                .ToListAsync();
        }
    }
}
