namespace BrandApplication.Business.DTOs
{
    public class ModelDto
    {
        public int ModelId { get; set; }
        public string ModelName { get; set; }

        public int BrandId { get; set; }
        public BrandDto Brand { get; set; }
    }
}
