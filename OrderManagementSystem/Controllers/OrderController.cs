using AutoMapper;
using DataAccess.DTO;
using DataAccess.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Repositories;
using Services;
using System.Data;

namespace OrderManagementSystem.Controllers
{
    [ApiController]
    [Route("api/[Controller]")]
    public class OrderController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IOrderService _orderService;

        public OrderController(IMapper mapper, IUnitOfWork unitOfWork, IOrderService orderservice)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _orderService = orderservice;
        }

        [HttpPost]
        public async Task<IActionResult> CreateOrder([FromBody] CreateOrder orderDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var orderMap = _mapper.Map<Order>(orderDto);
            var order = await _orderService.CreateOrderAsync(orderMap);
            var orderDtoMap = _mapper.Map<CreateOrder>(order);
            return Ok(orderDtoMap);
        }


        [HttpGet("{orderId}")]
        public async Task<IActionResult> GetOrderDetails([FromRoute] int orderId)
        {
            var orderdata = await _unitOfWork.Orders.FindAsync(o => o.OrderId == orderId);
            if (orderdata == null)
            {
                return NotFound("No Order Found");
            }

            var convertOrder = _mapper.Map<GetOrder>(orderdata);
            return Ok(convertOrder);
        }

        [Authorize(Roles = "Admin")]
        [HttpGet("")]
        public async Task< ActionResult<IEnumerable<GetAllOrders>>> GetAllOrders()
        {
            var allOrder = await _unitOfWork.Orders.GetAllAsync();
            if(allOrder == null)
            {
                return NotFound("No Orders Found");
            }
            var convertOrder = _mapper.Map<IEnumerable<GetAllOrders>>(allOrder);
            return Ok(convertOrder);
        }

        [Authorize(Roles = "Admin")]
        [HttpPut("{orderId}/status")]
        public async Task<IActionResult> UpdateStatusOrder([FromRoute] int orderId, [FromBody] Status status)
        {
            var order = await _unitOfWork.Orders.FindAsync(o => o.OrderId == orderId);
            if (order == null)
            {
                return NotFound("No order Found");
            }
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            _mapper.Map(status, order);
            order.OrderDate = DateTime.Now;
            _unitOfWork.Orders.Update(order);
            await _unitOfWork.complete();
            return Ok(order);
        } 


    }
}
