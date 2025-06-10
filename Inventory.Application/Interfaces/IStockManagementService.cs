using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inventory.Application.Interfaces
{
    public interface IStockManagementService
    {
        Task CheckAndNotifyLowStockAsync(int threshold);
        Task DecreaseCarStockAsync(int carId, int quantity);

    }
}
