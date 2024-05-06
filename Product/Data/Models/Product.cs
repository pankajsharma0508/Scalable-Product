using MongoDB.Bson;

namespace Product.Data.Models
{
    public class Product
    {
        public ObjectId Id { get; set; }

        public string ProductId => this.Id.ToString();

        public string Name { get; set; }

        public string Description { get; set; }

        public int AvailableQuantity {  get; set; }

        public decimal Price { get; set; }
    }
}
