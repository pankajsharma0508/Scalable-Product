using MediatR;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using Product.Data.Command;
using Product.Data.Models;
using Product.Data.Queries;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Product.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IMediator mediator;

        public ProductsController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        // GET: api/<ProductsController>
        [HttpGet]
        public async Task<List<Data.Models.Product>> Get() => await mediator.Send(new GetProductsQuery());


        // GET api/<ProductsController>/5
        [HttpGet("{id}")]
        public async Task<Data.Models.Product> Get(string id) => await mediator.Send(new GetProductQuery { Id = id });


        // POST api/<ProductsController>
        [HttpPost]
        public async Task<Data.Models.Product> Post([FromBody] Data.Models.Product product) => await mediator.Send(new CreateProductCommand { Product = product });


        // PUT api/<ProductsController>/5
        [HttpPut("{id}")]
        public async Task<Data.Models.Product> Put(int id, [FromBody] Data.Models.Product product) => await mediator.Send(new UpdateProductCommand { Product = product });

        // DELETE api/<ProductsController>/5
        [HttpDelete("{id}")]
        public async Task<bool> Delete(string id) => await mediator.Send(new DeleteProductCommand { Id = id });
    }
}
