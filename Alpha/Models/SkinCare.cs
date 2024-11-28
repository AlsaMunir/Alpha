using System.ComponentModel.DataAnnotations.Schema;

namespace Alpha.Models
{
    public class SkinCare
    {
        public int Id { get; set; }
        public string SkinCareName { get; set; }
        public string SkinCareDescription { get; set; }
        public double Price { get; set; }
        public string ImageUrl { get; set; }
        [NotMapped]
        public IFormFile CoverImage { get; set; }
    }
}
