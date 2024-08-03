using AutoMapper;
using DataAccess.DTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Repositories;
using Services;

namespace OrderManagementSystem.Controllers
{
    [ApiController]
    [Route("api/[Controller]")]
    public class UserController : ControllerBase
    {
        private readonly IAuthService _authService;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public UserController(IAuthService authService, IUnitOfWork unitOfWork, IMapper mapper)
        {
            _authService = authService;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        [HttpPost("register")]
        public async Task<IActionResult> registerAsync([FromBody] Register user)
        {
            if(!ModelState.IsValid)
                return BadRequest(ModelState);
            
            var result = await _authService.RegisterAsync(user);
           
            if (!result.IsAuthenticated)
                return BadRequest(result.Message);
            
            return Ok(result);
        }


        [HttpPost("login")]
        public async Task<IActionResult> LoginAsync([FromBody] Login user)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _authService.GetTokenAsync(user);

            if (!result.IsAuthenticated)
                return BadRequest(result.Message);


            return Ok(result);

        }

        [Authorize(Roles ="Admin")]
        [HttpPost("addrole")]
        public async Task<IActionResult> AddRoleAsync([FromBody] AddRole model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _authService.AddRoleAsync(model);

            if (!string.IsNullOrEmpty(result))
                return BadRequest(result);


            return Ok(model);

        }


    }
}
