using AutoMapper;
using DataAccess.DTO;
using DataAccess.Entities;
using Microsoft.AspNetCore.Mvc;
using Repositories;

namespace OrderManagementSystem.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CustomersController : ControllerBase
    {

        private readonly IUnitOfWork _unitofwork;
        private readonly IMapper _mapper;

        public CustomersController(IUnitOfWork unitofwork, IMapper mapper)
        {
            _unitofwork = unitofwork;
            _mapper = mapper;
        }

        [HttpPost]
        public async Task <IActionResult> CreateCustomer([FromBody] CreateCustomer customer)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }   
            var newCustomer = _mapper.Map<Customer>(customer);
            _unitofwork.Customers.AddAsync(newCustomer);
            _unitofwork.complete();
            return Ok(newCustomer);
        }

        [HttpGet("/{CustomerId}/Order")]
        public async Task<IActionResult> GetOrdersByCustomer([FromRoute] int CustomerId)
        {
            var order = await _unitofwork.Orders.FindAllAsync(c => c.CustomerId == CustomerId, new[] { "OrderItems", "Invoice" });
            if (order == null)
            {
                return NotFound("No Order Found by this Customer");
            }
            var map = _mapper.Map<IEnumerable<GetOrder>>(order);
            return Ok(map);
        }
    }
}