using MediatR;
using Microsoft.EntityFrameworkCore;
using MongoDB.Bson;
using MongoDB.Driver;
using Product.Data.Queries;

namespace Product.Data.Handlers
{
    public class GetProductQueryHandler : BaseRequestHandler, IRequestHandler<GetProductQuery, Models.Product?>
    {
        public Task<Models.Product?> Handle(GetProductQuery request, CancellationToken cancellationToken)
        {
            var _dbContext = GetDBContext();
            return _dbContext.Products.FindAsync(new ObjectId(request.Id)).AsTask();
        }
    }

    public class GetProductsQueryHandler : BaseRequestHandler, IRequestHandler<GetProductsQuery, List<Models.Product>>
    {
        public Task<List<Models.Product>> Handle(GetProductsQuery request, CancellationToken cancellationToken)
        {
            var db = GetDBContext();
            return db.Products.ToListAsync();
        }
    }
}
