using MediatR;
using MongoDB.Bson;

namespace Product.Data.Queries
{
    public class GetProductQuery: IRequest<Models.Product>
    {
        public string Id { get; set; }
    }

    public class GetProductsQuery : IRequest<List<Models.Product>>
    {

    }
}
