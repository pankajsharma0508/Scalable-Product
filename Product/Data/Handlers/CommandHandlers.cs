using MediatR;
using MongoDB.Bson;
using MongoDB.Driver;
using Product.Data.Command;

namespace Product.Data.Handlers
{

    public class CreateProductCommandHandler : BaseRequestHandler, IRequestHandler<CreateProductCommand, Models.Product>
    {
        public async Task<Models.Product> Handle(CreateProductCommand request, CancellationToken cancellationToken)
        {
            var _dbContext = GetDBContext();
            _dbContext.Products.Add(request.Product);
            await _dbContext.SaveChangesAsync();
            return request.Product;
        }
    }

    public class UpdateProductCommandHandler : BaseRequestHandler, IRequestHandler<UpdateProductCommand, Models.Product>
    {
        public async Task<Models.Product> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
        {
            var _dbContext = GetDBContext();
            var product = await _dbContext.Products.FindAsync(request.Product.Id);
            if (product != null)
            {
                // Update specific properties
                product.Name = request.Product.Name;
                await _dbContext.SaveChangesAsync();
            }
            return product;
        }
    }

    public class DeleteProductCommandHandler : BaseRequestHandler, IRequestHandler<DeleteProductCommand, bool>
    {
        public async Task<bool> Handle(DeleteProductCommand request, CancellationToken cancellationToken)
        {
            var _dbContext = GetDBContext();

            var product = await _dbContext.Products.FindAsync(new ObjectId(request.Id));
            if (product != null)
            {
                _dbContext.Products.Remove(product);
                await _dbContext.SaveChangesAsync();
            }
            return true;
        }
    }


    public class BaseRequestHandler
    {
        protected MongoDbContext GetDBContext()
        {
            var connectionString = "mongodb+srv://pankMongo:Mongo123@pankcluster0.jwx8zze.mongodb.net/?retryWrites=true&w=majority";
            if (connectionString == null)
            {
                Console.WriteLine("You must set your 'DB_URL' environment variable. ");
                Environment.Exit(0);
            }
            var client = new MongoClient(connectionString);
            return MongoDbContext.Create(client.GetDatabase("Product_DB"));
        }
    }
}
