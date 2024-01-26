using System.ComponentModel.DataAnnotations;

namespace BrandApplication.DataAccess.Models
{
    public class Brand
    {
        [Key]
        public int BrandId { get; set; }
        public string BrandName { get; set; }

        public ICollection<Model> Models { get; set; }
    }
}