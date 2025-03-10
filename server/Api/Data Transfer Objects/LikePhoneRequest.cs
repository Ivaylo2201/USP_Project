using System.ComponentModel.DataAnnotations;

namespace Api.Data_Transfer_Objects
{
    public class LikePhoneRequest
    {
        [Required(ErrorMessage = "Phone id is required.")]
        public int PhoneId { get; set; }
    }
}
