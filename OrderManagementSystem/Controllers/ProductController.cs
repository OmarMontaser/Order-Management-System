using AutoMapper;
using DataAccess.DTO;
using DataAccess.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Repositories;

namespace OrderManagementSystem.Controllers
{
    [ApiController]
    [Route("api/[Controller]")]
    public class ProductController : ControllerBase
    {
        private readonly IMapper _mapper; 
        private readonly IUnitOfWork _unitOfWork;

        public ProductController(IMapper mapper, IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        [HttpGet]
        public async Task<ActionResult> GetProducts(int page = 1 , int pageSize = 1)
        {
            if (page <= 0 || pageSize <= 0)
            {
                return BadRequest("Page and PageSize must be greater than zero.");
            }
            var skip = (page -1 ) * pageSize;

            var products = await _unitOfWork.Products.FindAllAsync(p => true, pageSize, skip);
            if (products == null)
            {
                return BadRequest("No Products Found");
            }

            var convertProduct = _mapper.Map<IEnumerable<GetAllProducts>>(products);
            return Ok(convertProduct);
        }


        [HttpGet("{productId}")]
        public async Task<IActionResult> GetProduct([FromRoute] int productId)

        {
            var product = await _unitOfWork.Products.FindAsync(p=>p.ProductId == productId);
            if (product == null)
            {
                return BadRequest("No Product Found");
            }

            var convertProduct = _mapper.Map<GetProduct>(product);

            var productDto = _mapper.Map<GetProductDto>(convertProduct);
            return Ok(productDto);
        }

        [Authorize(Roles ="Admin")]
        [HttpPost]
        public async Task<IActionResult> CreateProduct([FromBody] CreateProduct product)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var newProduct = _mapper.Map<Product>(product);
            await _unitOfWork.Products.AddAsync(newProduct);
            await _unitOfWork.complete();
            var productDto = _mapper.Map<CreateProduct>(newProduct);
            return Ok(productDto);
        }

        [Authorize(Roles = "Admin")]
        [HttpPut("{ProductId}")]
        public async Task<IActionResult> UpdateProduct([FromRoute] int ProductId, [FromBody] UpdateProduct upProduct)
        {
            var product = await _unitOfWork.Products.FindAsync(p => p.ProductId == ProductId);
            if (product == null)
            {
                return BadRequest("No product Found");
            }
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            _mapper.Map(upProduct, product);
            _unitOfWork.Products.Update(product);
            await _unitOfWork.complete();
            return Ok(product);
        }

    }
}
