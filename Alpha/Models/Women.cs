using System.ComponentModel.DataAnnotations.Schema;

namespace Alpha.Models
{
    public class Women
    {
        public int Id { get; set; }
        public string WomenOutfitName { get; set; }
        public string WomenOutfitDescription { get; set; }
        public double Price { get; set; }
        public string ImageUrl { get; set; }
        [NotMapped]
        public IFormFile CoverImage { get; set; }
    }
}
