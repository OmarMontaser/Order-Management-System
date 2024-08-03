using DataAccess.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.DTO
{
    public class CreateOrder
    {
        public int CustomerId { get; set; }
        [Precision(18, 2)]
        public decimal TotalAmount { get; set; }
        public string PaymentMethod { get; set; }
        public string Status { get; set; }
        public ICollection<OrderItemDTO> OrderItems { get; set; }
        public Invoice Invoice { get; set; }
    }
}