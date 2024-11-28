using System.ComponentModel.DataAnnotations.Schema;

namespace Alpha.Models
{
    public class Accessories
    {
        public int Id { get; set; }
        public string AccessoriesName { get; set; }
        public string AccessoriesDescription { get; set; }
        public double Price { get; set; }
        public string ImageUrl { get; set; }
        [NotMapped]
        public IFormFile CoverImage { get; set; }
    }
}
