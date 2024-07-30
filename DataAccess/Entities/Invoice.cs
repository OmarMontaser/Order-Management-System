using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Entities
{
    public class Invoice
    {
        public int InvoiceId { get; set; }

        [ForeignKey("Order")]
        public int OrderId { get; set; }
        public Order Order { get; set; }

        public DateTime InvoiceDate { get; set; }
      
        [Precision(18,2)]
        public decimal InvoiceAmount { get; set; }
    }
}
