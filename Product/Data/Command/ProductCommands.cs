using MediatR;
using MongoDB.Bson;

namespace Product.Data.Command
{
    public class DeleteProductCommand: IRequest<bool>
    {
        public string Id { get; set; }
    }

    public class CreateProductCommand : IRequest<Models.Product>
    {
        public Models.Product Product { get; set; }
    }

    public class UpdateProductCommand : IRequest<Models.Product>
    {
        public Models.Product Product { get; set; }
    }
}
