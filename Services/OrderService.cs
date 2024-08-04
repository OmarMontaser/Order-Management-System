using AutoMapper;
using DataAccess.DTO;
using DataAccess.Entities;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class OrderService : IOrderService
    {
        private readonly IUnitOfWork _unitofwork;
        private readonly IMapper _mapper;
        private readonly IInvoiceService _invoiceService;

        public OrderService(IUnitOfWork unitofwork, IMapper mapper , IInvoiceService invoiceService)
        {
            _unitofwork = unitofwork;
            _mapper = mapper;
            _invoiceService = invoiceService;
        }

        public async Task<Order> CreateOrderAsync(Order order)
        {
            foreach (var item in order.OrderItems) {

                var product = await _unitofwork.Products.FindAsync( i => i.ProductId == item.ProductId);
                if(product is null || product.Stock < item.Quantity)
                {
                    throw new Exception("Insufficient stock for product " + product?.Name);
                }
                product.Stock-= item.Quantity;
                _unitofwork.Products.Update(product);
                await _unitofwork.complete();
            }

            var totalAmount = order.OrderItems.Sum(item => item.Quantity * item.UnitPrice);
            if (totalAmount > 200) 
            {
                totalAmount *= 0.9M;
            }
            else if (totalAmount >100)
            {
                totalAmount *= 0.95M;
            }
            order.TotalAmount = totalAmount;
            
            await _unitofwork.Orders.AddAsync(order);
            await _unitofwork.complete();

            await _invoiceService.GenerateInvoiceAsync(order);

            return order;
        }


    }
}
