using Inventory.Application.Interfaces;
using Inventory.Core.Entities;
using Inventory.Core.Interfaces;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inventory.Application.Services
{
    public class StockManagementService : IStockManagementService
    {
        private readonly ICarRepository _carRepository;
        private readonly INotificationService _notificationService; // Bildiriş göndərən xidmət

        public StockManagementService(ICarRepository carRepository, INotificationService notificationService)
        {
            _carRepository = carRepository;
            _notificationService = notificationService;
        }

        // Stok miqdarını yoxlayın və azalan məhsullar haqqında xəbərdarlıq göndərin
        public async Task CheckAndNotifyLowStockAsync(int threshold)
        {
            var lowStockCars = await _carRepository.GetLowStockCarsAsync(threshold);

            string filePath = @"C:\excel-export-inventory\LowStockCars.xlsx";
            ExportCarsToExcel(lowStockCars, filePath);

            foreach (var car in lowStockCars)
            {
                // Bildiriş göndərilməsi
                await _notificationService.SendLowStockNotificationAsync(car);
            }
        }

        // Avtomobilin stokunu azaldıqda bu metodu çağırın
        public async Task DecreaseCarStockAsync(int carId, int quantity)
        {
            var car = await _carRepository.GetCarByIdAsync(carId);
            if (car != null && car.StockQuantity >= quantity)
            {
                car.StockQuantity -= quantity;
                await _carRepository.UpdateCarAsync(car);

                // Stok azaldıqdan sonra yoxlama aparın
                await CheckAndNotifyLowStockAsync(5); // 5-dən az olan avtomobillər barədə xəbərdarlıq
            }
        }

        public void ExportCarsToExcel(IEnumerable<Car> cars, string filePath)
        {
            try
            {
                // Set the license context before using EPPlus
                ExcelPackage.License.SetNonCommercialPersonal("Yashar Isgenderov");

                // EPPlus to generate the Excel file
                using (var package = new ExcelPackage())
                {
                    // Adding a new worksheet to the workbook
                    var worksheet = package.Workbook.Worksheets.Add("Low Stock Cars");

                    // Add column headers
                    worksheet.Cells[1, 1].Value = "Brand";
                    worksheet.Cells[1, 2].Value = "Model";
                    worksheet.Cells[1, 3].Value = "Year";
                    worksheet.Cells[1, 4].Value = "Price";
                    worksheet.Cells[1, 5].Value = "Stock Quantity";

                    // Add data to cells
                    int row = 2;  // Starting row after the header row
                    foreach (var car in cars)
                    {
                        worksheet.Cells[row, 1].Value = car.Brand;
                        worksheet.Cells[row, 2].Value = car.Model;
                        worksheet.Cells[row, 3].Value = car.Year;
                        worksheet.Cells[row, 4].Value = car.Price;
                        worksheet.Cells[row, 5].Value = car.StockQuantity;
                        row++;
                    }

                    // Ensure the directory exists for saving the file
                    var directory = Path.GetDirectoryName(filePath);
                    if (!Directory.Exists(directory))
                    {
                        Directory.CreateDirectory(directory); // Create directory if it doesn't exist
                    }

                    // Save the Excel file to the specified path
                    var fileInfo = new FileInfo(filePath);
                    package.SaveAs(fileInfo);
                }

                Console.WriteLine($"Low stock cars data has been exported to {filePath}");
            }
            catch (Exception ex)
            {
                // Catch any errors and output to console for debugging
                Console.WriteLine($"Error exporting to Excel: {ex.Message}");
            }
        }
    }
    }
