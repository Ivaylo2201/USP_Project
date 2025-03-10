using System.ComponentModel.DataAnnotations;

namespace Api.Data_Transfer_Objects
{
    public class CreatePhoneBody
    {
        [Required(ErrorMessage = "BrandId is required.")]
        public int BrandId { get; set; }

        [Required(ErrorMessage = "ModelId is required.")]
        public int ModelId { get; set; }

        [Required(ErrorMessage = "ColorId is required.")]
        public int ColorId { get; set; }

        [Range(1, 5000, ErrorMessage = "Price must be between 1 and 5000.")]
        public decimal Price { get; set; }
    }
}
