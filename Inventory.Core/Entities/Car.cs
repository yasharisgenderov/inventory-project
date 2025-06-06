using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inventory.Core.Entities
{
    public class Car
    {
        public int Id { get; set; } // Unique identifier for the car
        public string? Brand { get; set; } // Car's brand (e.g., BMW, Audi)
        public string? Model { get; set; } // Car's model (e.g., X5, A6)
        public int Year { get; set; } // Year of manufacture
        public decimal Price { get; set; } // Price of the car
        public int StockQuantity { get; set; } // How many cars of this model are available in stock
        public string? Condition { get; set; } // Condition of the car ("New", "Used")
    }
}
