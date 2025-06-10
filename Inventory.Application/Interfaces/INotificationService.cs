using Inventory.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inventory.Application.Interfaces
{
    public interface INotificationService
    {
        Task SendLowStockNotificationAsync(Car car);
    }
}
