using Akb.Base.Response;
using Akb.Business.Cqrs;
using Akb.Schema;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Akb.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AddressesController : ControllerBase
    {
        private readonly IMediator mediator;

        public AddressesController(IMediator mediator)
        {
            this.mediator = mediator;

        }


        // GET: api/<CustomersController>
        [HttpGet]
        public async Task<ApiResponse<List<AddressResponse>>> Get()
        {
            var operation = new GetAllAddressQuery();
            var result = await mediator.Send(operation);
            return result;
        }

        // GET api/<CustomersController>/5
        [HttpGet("{id}")]
        public async Task<ApiResponse<AddressResponse>> GetbyId(int id)
        {
            var operation = new GetAddressByIdQuery(id);
            var result = await mediator.Send(operation);
            return result;
        }

        [HttpGet("GetByParameter")]
        public async Task<ApiResponse<List<AddressResponse>>> GetByParameter(string? Country, string? City, string? Address1)
        {
            var operation = new GetAddressByParameterQuery(Country, City, Address1);
            var result = await mediator.Send(operation);
            return result;
        }

        // POST api/<CustomersController>
        [HttpPost]
        public async Task<ApiResponse<AddressResponse>> Post([FromBody] AddressRequest address)
        {
            var operation = new CreateAddressCommand(address);
            var result = await mediator.Send(operation);
            return result;
        }

        // PUT api/<CustomersController>/5
        [HttpPut("{id}")]
        public async Task<ApiResponse> Put(int id, [FromBody] AddressRequest address)
        {
            address.Id = id;
            var operation = new UpdateAddressCommand(id, address);
            var result = await mediator.Send(operation);
            return result;

        }

        // DELETE api/<CustomersController>/5
        [HttpDelete("{id}")]
        public async Task<ApiResponse> Delete(int id)
        {
            var operation = new DeleteAddressCommand(id);
            var result = await mediator.Send(operation);
            return result;
        }
    }
}
