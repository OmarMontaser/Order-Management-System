using DataAccess.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _context;

        public IRepository<Customer> Customers { get; private set; }
        public IRepository<Invoice> Invoices { get; private set; }
        public IRepository<Order> Orders { get; private set; }
        public IRepository<OrderItem> OrderItems { get; private set; }
        public IRepository<Product> Products { get; private set; }

        public UnitOfWork(ApplicationDbContext context)
        {
            _context = context;

            Customers = new Repository<Customer>(_context);
            Invoices = new Repository<Invoice>(_context);
            Orders = new Repository<Order>(_context);
            OrderItems = new Repository<OrderItem>(_context);
            Products = new Repository<Product>(_context);
        }

        public async Task<int> complete()
        {
            return await _context.SaveChangesAsync();
        }

        public void Dispose()
        {
              _context.Dispose();
        }
    }
}
