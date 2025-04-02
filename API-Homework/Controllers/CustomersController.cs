using API_Homework.Dtos;
using API_Homework.Entities;
using API_Homework.Repositories.Abstract;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace API_Homework.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomersController : ControllerBase
    {
        private readonly ICustomerRepository _customerRepository;

        public CustomersController(ICustomerRepository customerRepository)
        {
            _customerRepository = customerRepository;
        }

        // GET: api/<CustomersController>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CustomerDto>>> Get()
        {
            var customers=await _customerRepository.GetAll();
            var data = customers.Select(s => new CustomerDto
            {
                Id = s.Id,
                Name = s.Name,
                Surname = s.Surname,             
            });
            return Ok(data);
        }

        // GET api/<CustomersController>/5
        [HttpGet("{id}")]
        public async Task<ActionResult<CustomerDto>> Get(int id)
        {
            var item = await _customerRepository.Get(a => a.Id == id);
            if (item == null)
            {
                return NotFound();
            }
            var data = new CustomerDto
            {
                Id = item.Id,
                Name = item.Name,
                Surname = item.Surname,
            };
            return Ok(data);
        }

        // POST api/<CustomersController>
        [HttpPost]
        public async Task<ActionResult<CustomerDto>> Post([FromBody] CustomerAddDto item)
        {
            var customer = new Customer
            {
                Name = item.Name,
                Surname = item.Surname,
            };
            var returnedData=await _customerRepository.Add(customer);

            var data = new CustomerDto
            {
                Id = returnedData.Id,
                Name = returnedData.Name,
                Surname = returnedData.Surname,
            };
            return Ok(data);
        }

        // PUT api/<CustomersController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<CustomersController>/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<CustomerDto>> Delete(int id)
        {
            var item= await _customerRepository.Get(a => a.Id == id);
            if (item == null)
            {
                return NotFound();
            }
            await _customerRepository.Delete(item);
            return Ok();
        }
    }
}
