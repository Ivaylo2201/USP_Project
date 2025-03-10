using System.ComponentModel.DataAnnotations;

namespace Api.Data_Transfer_Objects
{
    public class RemovePhoneFromCartRequest
    {
        [Required(ErrorMessage = "Item id is required.")]
        public int ItemId { get; set; }
    }
}
