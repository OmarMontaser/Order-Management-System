using DataAccess.Entities;
using Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class InvoiceService : IInvoiceService
    {
        private readonly IUnitOfWork _unitOfWork;

        public InvoiceService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task GenerateInvoiceAsync(Order order)
        {
            var invoice = new Invoice
            {
                OrderId = order.OrderId,
                InvoiceDate = DateTime.Now,
                InvoiceAmount = order.TotalAmount,
            };
            _unitOfWork.Invoices.AddAsync(invoice);
            await _unitOfWork.complete();
        }
    }
}

