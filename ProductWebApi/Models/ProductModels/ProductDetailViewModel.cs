using System.ComponentModel.DataAnnotations;

namespace ProductWebApi.Models
{
    public class ProductDetailViewModel
    {
        [Required]
        [Range(1, 5, ErrorMessage = "Category Id 1 ile 5 arasındadır.")]
        public string Category { get; set; }
        public string Brand { get; set; }
        public string Name { get; set; }
        public string Size { get; set; }
        public int Price { get; set; }
        public int Stock { get; set; }
    }
}
