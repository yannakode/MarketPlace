using System.ComponentModel.DataAnnotations.Schema;

namespace MarketPlace.Models
{
    public class Product
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }

        [NotMapped]
        public IFormFile Image { get; set; }
    }
}
