using Inventory.Application.DTOs;
using Inventory.Application.Interfaces;
using Inventory.Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace Inventory.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SaleController : ControllerBase
    {
        private readonly ISalesService _salesService;

        public SaleController(ISalesService salesService)
        {
            _salesService = salesService;
        }

        // GET: api/Sale
        [HttpGet]
        public async Task<IActionResult> GetSales()
        {
            var sales = await _salesService.GetAllSalesAsync();
            return Ok(sales);  // Returns all sales
        }

        // GET: api/Sale/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> GetSale(int id)
        {
            var sale = await _salesService.GetSaleByIdAsync(id);
            if (sale == null)
            {
                return NotFound();  // Return 404 if the sale is not found
            }
            return Ok(sale);  // Return the sale with the given ID
        }

        // POST: api/Sale
        [HttpPost]
        public async Task<IActionResult> AddSale([FromBody] SaleDto saleDto)
        {
            await _salesService.AddSaleAsync(saleDto);
            return CreatedAtAction(nameof(GetSale), new { id = saleDto.Id }, saleDto);  // Return 201 Created with the sale
        }

        // PUT: api/Sale/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateSale(int id, [FromBody] SaleDto saleDto)
        {
            if (id != saleDto.Id)
            {
                return BadRequest("Sale ID mismatch");  // Return 400 Bad Request if IDs don't match
            }

            await _salesService.UpdateSaleAsync(saleDto);
            return NoContent();  // Return 204 No Content if the update is successful
        }

        // DELETE: api/Sale/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSale(int id)
        {
            await _salesService.DeleteSaleAsync(id);
            return NoContent();  // Return 204 No Content if the deletion is successful
        }

        // GET: api/v1/inventory/sales/{customerId}/revenue
        [HttpGet("{customerId}/revenue")]
        public async Task<IActionResult> GetTotalRevenueByCustomerId(int customerId)
        {
            var totalRevenue = await _salesService.GetTotalRevenueByCustomerIdAsync(customerId);
            return Ok(totalRevenue);
        }

        // GET: api/v1/inventory/sales/{customerId}/popular-brand
        [HttpGet("{customerId}/popular-brand")]
        public async Task<IActionResult> GetMostPopularCarBrandByCustomerId(int customerId)
        {
            var popularBrand = await _salesService.GetMostPopularCarBrandByCustomerIdAsync(customerId);
            return Ok(popularBrand);
        }

        // GET: api/v1/inventory/sales/{customerId}/popular-model
        [HttpGet("{customerId}/popular-model")]
        public async Task<IActionResult> GetMostPopularCarModelByCustomerId(int customerId)
        {
            var popularModel = await _salesService.GetMostPopularCarModelByCustomerIdAsync(customerId);
            return Ok(popularModel);
        }
    }
}
