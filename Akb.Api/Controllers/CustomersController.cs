using Akb.Base.Response;
using Akb.Business.Cqrs;
using Akb.Schema;
using MediatR;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Akb.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomersController : ControllerBase
    {
        private readonly IMediator mediator;

        public CustomersController(IMediator mediator)
        {
            this.mediator = mediator;

        }


        // GET: api/<CustomersController>
        [HttpGet]
        public async Task<ApiResponse<List<CustomerResponse>>> Get()
        {
            var operation = new GetAllCustomerQuery();
            var result = await mediator.Send(operation);
            return result;
        }

        [HttpGet("GetByParameter")]
        public async Task<ApiResponse<List<CustomerResponse>>> GetbyParameter([FromQuery] string? firstName, string? lastName, string? IdentityNumber)
        {
            var operation = new GetCustomerByParameterQuery(firstName, lastName, IdentityNumber);
            var result = await mediator.Send(operation);
            return result;
        }


        // GET api/<CustomersController>/5
        [HttpGet("{id}")]
        public async Task<ApiResponse<CustomerResponse>> GetbyId(int id)
        {
            var operation = new GetCustomerByIdQuery(id);
            var result = await mediator.Send(operation);
            return result;
        }

        // POST api/<CustomersController>
        [HttpPost]
        public async Task<ApiResponse<CustomerResponse>> Post([FromBody] CustomerRequest customer)
        {
            var operation = new CreateCustomerCommand(customer);
            var result = await mediator.Send(operation);
            return result;
        }

        // PUT api/<CustomersController>/5
        [HttpPut("{id}")]
        public async Task<ApiResponse> Put(int id, [FromBody] CustomerRequest customer)
        {
            customer.CustomerNumber = id;
            var operation = new UpdateCustomerCommand(id, customer);
            var result = await mediator.Send(operation);
            return result;

        }

        // DELETE api/<CustomersController>/5
        [HttpDelete("{id}")]
        public async Task<ApiResponse> Delete(int id)
        {
            var operation = new DeleteCustomerCommand(id);
            var result = await mediator.Send(operation);
            return result;
        }
    }
}
