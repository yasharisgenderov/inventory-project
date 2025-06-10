using Inventory.Application.Interfaces;
using Inventory.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inventory.Application.Services
{
    public class NotificationService : INotificationService
    {
        public async Task SendLowStockNotificationAsync(Car car)
        {
            // Burada bildiriş göndərilməsi üçün konkret metodlardan istifadə olunacaq
            // Məsələn, Email, SMS, ya da sistem daxilində bildiriş

            var message = $"Warning: The stock for {car.Brand} {car.Model} {car.Year} is low. Only {car.StockQuantity} left in stock!";
            // Bu mesajı adminə göndərmək
            Console.WriteLine(message);  // Burada sadəcə Console ilə göstəririk. Real bildiriş göndəriləcək.
        }
    }
}
