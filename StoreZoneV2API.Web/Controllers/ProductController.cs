using Microsoft.AspNetCore.Mvc;
using StoreZoneV2API.Application.Services;
using StoreZoneV2API.Domain.Entities;

namespace StoreZoneV2API.Web.Controllers
{
    //add this today friday in 9:44 AM...
    [ApiController]
    [Route("api/[controller]")]
    public class ProductController : ControllerBase
    {
        private readonly ProductService _productService;
        public ProductController(ProductService productService)
        {
            _productService = productService;
        }
        // GET: api/product/GetAll
        [HttpGet("GetAll")]
        public async Task<ActionResult<IEnumerable<Product>>> GetAll() => Ok(await _productService.GetAllAsync());

        // GET: api/product/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<Product>> GetById(Guid id)
        {
           var product = await _productService.GetByIdAsync(id);
            return product is null ? NotFound() : Ok(product);
        }


        // POST: api/product/create
        [HttpPost("create")]
        public async Task<ActionResult<Guid>> Create([FromBody] ProductDto dto)
        {
           if (dto is null)
            {
                return BadRequest();
            }
            var id = await _productService.CreateAsync(dto.Name, dto.Price);
            return CreatedAtAction(nameof(GetById), new { id }, null);

        }

        public record ProductDto(string Name , decimal Price); 
    }
}
