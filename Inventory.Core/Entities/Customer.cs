using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inventory.Core.Entities
{
    public class Customer
    {
        public int Id { get; set; } // Unique identifier for the customer
        public string? Name { get; set; } // Customer's name
        public string? Contact { get; set; } // Customer's contact details (e.g., phone number or email)
    }
}
