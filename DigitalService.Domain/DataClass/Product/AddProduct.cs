using System.ComponentModel.DataAnnotations;

namespace DigitalService.Domain.DataClass
{
    public class AddProduct
    {
        [Required]
        public string? ProductName { get; set; }

        [Required]
        public string? ProductDescription { get; set; }

        public string? ImgUrl { get; set; }

        [Required]
        public bool ProductIsActive { get; set; } = true;

        public DateTime? CreatedDate { get; set; } = DateTime.Now;
    }
}