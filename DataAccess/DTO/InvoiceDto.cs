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
    public class InvoiceDto
    {
        public int InvoiceId { get; set; }

        public int OrderId { get; set; }

        public DateTime InvoiceDate { get; set; }

        [Precision(18, 2)]
        public decimal InvoiceAmount { get; set; }
    }
}