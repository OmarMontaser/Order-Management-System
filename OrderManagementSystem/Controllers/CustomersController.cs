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
            var customer = await _unitofwork.Customers.FindAsync(c => c.CustomerId == CustomerId);
            if (customer == null)
            {
                return NotFound("No Order Found by this Customer");
            }
            var convertCustomer = _mapper.Map<GetOrderbyCustomer>(customer);
            return Ok(convertCustomer);
        }

    }
}
