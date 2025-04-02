using API_Homework.Dtos;
using API_Homework.Entities;
using API_Homework.Repositories.Abstract;
using API_Homework.Repositories.Concrete;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace API_Homework.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IProductRepository _productRepository;

        public ProductsController(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }


        // GET: api/<ProductsController>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProductDto>>> Get()
        {
            var products = await _productRepository.GetAll();
            var data = products.Select(s => new ProductDto
            {
                Id = s.Id,
                Name = s.Name,
                Discount = s.Discount,
                Price = s.Price,
            });
            return Ok(data);
        }

        // GET api/<ProductsController>/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ProductDto>> Get(int id)
        {
            var item = await _productRepository.Get(a => a.Id == id);
            if (item == null)
            {
                return NotFound();
            }
            var data = new ProductDto
            {
                Id = item.Id,
                Name = item.Name,
                Discount = item.Discount,
                Price = item.Price,
            };
            return Ok(data);
        }

        [HttpGet("GetHigherPrices")]
        public async Task<ActionResult<IEnumerable<ProductDto>>> GetHigherPrices()
        {
            var products = await _productRepository.GetAll();
            var topPrices = products
                .OrderByDescending(p => p.Price)
                .Take(3)
                .Select(s => new ProductDto
                {
                    Id = s.Id,
                    Name = s.Name,
                    Discount = s.Discount,
                    Price = s.Price
                });

            return Ok(topPrices);
        }

        [HttpGet("GetHigherDiscounts")]
        public async Task<ActionResult<IEnumerable<ProductDto>>> GetHigherDiscounts()
        {
            var products = await _productRepository.GetAll();
            var topDiscounts = products
                .OrderByDescending(p => p.Discount)
                .Take(3)
                .Select(s => new ProductDto
                {
                    Id = s.Id,
                    Name = s.Name,
                    Discount = s.Discount,
                    Price = s.Price
                });

            return Ok(topDiscounts);
        }

        // POST api/<ProductsController>
        [HttpPost]
        public async Task<ActionResult<ProductDto>> Post([FromBody] ProductAddDto item)
        {
            var product = new Product
            {
                Name = item.Name,
                Discount = item.Discount,
                Price = item.Price,
            };

            var returnedData = await _productRepository.Add(product);
            var data = new ProductDto
            {
                Id = returnedData.Id,
                Name = returnedData.Name,
                Discount = returnedData.Discount,
            };
            return Ok(data);
        }

        // PUT api/<ProductsController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<ProductsController>/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<CustomerDto>> Delete(int id)
        {
            var item = await _productRepository.Get(a => a.Id == id);
            if (item == null)
            {
                return NotFound();
            }
            await _productRepository.Delete(item);
            return Ok();
        }
    }
}
