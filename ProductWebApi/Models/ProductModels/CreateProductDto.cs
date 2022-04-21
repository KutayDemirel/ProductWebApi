using System;
using System.ComponentModel.DataAnnotations;

namespace ProductWebApi.Models
{
    public class CreateProductDto
    {
        [Required]
        [Range(1,7, ErrorMessage =" Lütfen uygun değer giriniz")]
        public int CategoryId{ get; set; }

        [Required]
        [Range(1,4, ErrorMessage =" Lütfen uygun değer giriniz")]
        public int BrandId { get; set; }

        [Required]
        [StringLength(50, ErrorMessage = "Ürün ismi en fazla 50 karakter olabilir", MinimumLength = 2)]
        public string Name { get; set; }

        [Required]
        [RegularExpression("Large|Medium|Small")]
        public string Size { get; set; }

        [Required]
        [Range(1, Int32.MaxValue, ErrorMessage = "Lütfen uygun değer giriniz.")]
        public int Price { get; set; }

        [Required]
        [Range(0, Int32.MaxValue, ErrorMessage = "Lütfen uygun değer giriniz.")]
        public int Stock { get; set; }
    }
}
