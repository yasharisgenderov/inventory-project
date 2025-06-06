using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inventory.Core.Entities
{
    public class Sale
    {
        public int Id { get; set; } // Unique identifier for the sale
        public int CarId { get; set; } // Foreign key to the Car entity
        public int CustomerId { get; set; } // Foreign key to the Customer entity
        public DateTime SaleDate { get; set; } // Date when the sale occurred
        public decimal SalePrice { get; set; } // The price at which the car was sold

        // Navigation properties (optional, depending on your ORM setup)
        public Car? Car { get; set; } // Associated Car entity
        public Customer? Customer { get; set; } // Associated Customer entity
    }
}
