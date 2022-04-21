using Microsoft.AspNetCore.Mvc;
using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace ProductWebApi.Models
{
    public class UpdateProductDto
    {
        [StringLength(50, ErrorMessage = "Ürün ismi en fazla 50 karakter olabilir", MinimumLength = 2)]
        public string Name { get; set; }
        [RegularExpression("Large|Medium|Small")]
        public string Size { get; set; }
       
        [Range(0, Int32.MaxValue, ErrorMessage ="Lütfen uygun değer giriniz.")]
        public int Price { get; set; }
        
        [Range(0, Int32.MaxValue, ErrorMessage ="Lütfen uygun değer giriniz.")]
        public int Stock { get; set; }
    }
}
