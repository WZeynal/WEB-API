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
    public class OrdersController : ControllerBase
    {
        private readonly IOrderRepository _orderRepository;

        public OrdersController(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }

        // GET: api/<OrdersController>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<OrderDto>>> Get()
        {
            var orders = await _orderRepository.GetAll();
            var data = orders.Select(s => new OrderDto
            {
                CustomerId = s.CustomerId,
                OrderDate = s.OrderDate,
                ProductId = s.ProductId
            });
            return Ok(data);
        }

        // GET api/<OrdersController>/5
        [HttpGet("{id}")]
        public async Task<ActionResult<OrderDto>> Get(int id)
        {
            var item = await _orderRepository.Get(a => a.Id == id);
            if (item == null)
            {
                return NotFound();
            }
            var data = new OrderDto
            {
                CustomerId = item.CustomerId,
                OrderDate = item.OrderDate,
                ProductId = item.ProductId
            };
            return Ok(data);
        }

        // POST api/<OrdersController>
        [HttpPost]
        public async Task<ActionResult<OrderDto>> Post([FromBody] OrderAddDto item)
        {
            var order = new Order
            {
                OrderDate = item.OrderDate,
                CustomerId = item.CustomerId,
                ProductId = item.ProductId
            };

            var dataReturn=_orderRepository.Add(order);

            var data = new OrderDto
            {
                CustomerId = item.CustomerId,
                OrderDate = item.OrderDate,
                ProductId = item.ProductId
            };
            return Ok(data);
        }

        // PUT api/<OrdersController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<OrdersController>/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<CustomerDto>> Delete(int id)
        {
            var item = await _orderRepository.Get(a => a.Id == id);
            if (item == null)
            {
                return NotFound();
            }
            await _orderRepository.Delete(item);
            return Ok();
        }
    }
}
