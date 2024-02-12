using System.ComponentModel.DataAnnotations;

namespace BrandApplication.DataAccess.Models
{
    public class ProductModel
    {
        [Key]
        public int ModelId { get; set; }
        public string ModelName { get; set; }

        public int BrandId { get; set; }
        public Brand Brand { get; set; }
    }
}
