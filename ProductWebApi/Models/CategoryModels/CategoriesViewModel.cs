using System.ComponentModel.DataAnnotations;

namespace ProductWebApi.Models.CategoryModels
{
    public class CategoriesViewModel
    {
        //public int CategoryId { get; set; }
        [Required]
        [StringLength(50, ErrorMessage = "Kategori ismi en fazla 50 karakter olabilir", MinimumLength = 2)]
        public string Name { get; set; }
        [Required]
        [StringLength(50, ErrorMessage = "Stil ismi en fazla 50 karakter olabilir", MinimumLength = 2)]
        public string Style { get; set; }
    }
}
