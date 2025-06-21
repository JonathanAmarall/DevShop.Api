using Microsoft.AspNetCore.Mvc;
using DevShop.Api.Models;
using DevShop.Api.Views;
using DevShop.Api.Repositories;
using AutoMapper;

namespace DevShop.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductsController : ControllerBase
    {
        private readonly IProductRepository _productRepository;
        private readonly IMapper _mapper;

        public ProductsController(IProductRepository productRepository, IMapper mapper)
        {
            _productRepository = productRepository;
            _mapper = mapper;
        }

        // GET: api/products
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProductView>>> GetProducts()
        {
            var products = await _productRepository.GetAllAsync();
            return _mapper.Map<List<ProductView>>(products);
        }

        // GET: api/products/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ProductView>> GetProduct(string id)
        {
            var product = await _productRepository.GetByIdAsync(id);

            if (product == null)
            {
                return NotFound();
            }

            return _mapper.Map<ProductView>(product);
        }

        // GET: api/products/code/{code}
        [HttpGet("code/{code}")]
        public async Task<ActionResult<ProductView>> GetProductByCode(string code)
        {
            var product = await _productRepository.GetByCodeAsync(code);

            if (product == null)
            {
                return NotFound();
            }

            return _mapper.Map<ProductView>(product);
        }

         // GET: api/products/name/{name}
        [HttpGet("name/{name}")]
        public async Task<ActionResult<ProductView>> GetProductByName   (string name)
        {
            var product = await _productRepository.GetByNameAsync(name);

            if (product == null)
            {
                return NotFound();
            }

            return _mapper.Map<ProductView>(product);
        }

        // GET: api/products/price-range
        [HttpGet("price-range")]
        public async Task<ActionResult<IEnumerable<ProductView>>> GetProductsByPriceRange(
            [FromQuery] decimal minPrice, [FromQuery] decimal maxPrice)
        {
            var products = await _productRepository.GetByPriceRangeAsync(minPrice, maxPrice);
            return _mapper.Map<List<ProductView>>(products);
        }

        // GET: api/products/low-stock
        [HttpGet("low-stock")]
        public async Task<ActionResult<IEnumerable<ProductView>>> GetLowStockProducts(
            [FromQuery] long threshold = 10)
        {
            var products = await _productRepository.GetLowStockAsync(threshold);
            return _mapper.Map<List<ProductView>>(products);
        }

        // POST: api/products
        [HttpPost]
        public async Task<ActionResult<ProductView>> CreateProduct(CreateProductView createProductView)
        {
            var product = _mapper.Map<Product>(createProductView);
            
            var createdProduct = await _productRepository.AddAsync(product);
            var productView = _mapper.Map<ProductView>(createdProduct);
            
            return CreatedAtAction(nameof(GetProduct), new { id = createdProduct.Id }, productView);
        }

        // PUT: api/products/5
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateProduct(string id, UpdateProductView updateProductView)
        {
            var product = await _productRepository.GetByIdAsync(id);
            if (product == null)
            {
                return NotFound();
            }

            _mapper.Map(updateProductView, product);
            await _productRepository.UpdateAsync(product);

            return NoContent();
        }

        // DELETE: api/products/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduct(string id)
        {
            var product = await _productRepository.GetByIdAsync(id);
            if (product == null)
            {
                return NotFound();
            }

            await _productRepository.DeleteAsync(product);

            return NoContent();
        }
    }
} 