using DataAccess.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public  interface IInvoiceService
    {
        Task GenerateInvoiceAsync(Order order);
    }
}
