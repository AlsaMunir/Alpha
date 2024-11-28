using System.ComponentModel.DataAnnotations.Schema;

namespace Alpha.Models
{
    public class Men
    {
        public int Id { get; set; }
        public string MenOutfitName { get; set; }
        public string MenOutfitDescription { get; set; }
        public double Price { get; set; }
        public string ImageUrl { get; set; }
        [NotMapped]
        public IFormFile CoverImage { get; set; }
    }
}
