namespace BrandApplication.Business.DTOs
{
    public class BrandDto
    {
        public int BrandId { get; set; }
        public string BrandName { get; set; }

        public ICollection<ModelDto> Models { get; set; }

    }
}
