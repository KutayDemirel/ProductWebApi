using ProductWebApi.Entities;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace ProductWebApi
{
    public class Product : BaseEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public int BrandId { get; set; }
        public string Name { get; set; }
        public string Size { get; set; }
        public int Price { get; set; }
        public int Stock { get; set; }

        //[ForeignKey("Category")]
        [JsonIgnore]
        public virtual Category Category {get; set;}
        public int CategoryId { get; set; }
    }
}
