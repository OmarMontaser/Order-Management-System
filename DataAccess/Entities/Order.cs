using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Entities
{
    public class Order
    {
        public int OrderId { get; set; }

        [ForeignKey("Customer")]
        public int CustomerId { get; set; } 
        public Customer Customer { get; set; }

        public DateTime OrderDate { get; set; } = DateTime.Now;

        [Precision(18, 2)]
        public decimal TotalAmount { get; set; }
        public ICollection<OrderItem> OrderItems { get; set; }
        public  string PaymentMethod { get; set; }
        public string Status { get; set; }
        public Invoice Invoice { get; set; }
    }
}
