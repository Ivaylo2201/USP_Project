namespace Api.Data_Transfer_Objects
{
    public class CreatePhoneBody
    {
        public decimal Price { get; set; }
        public int BrandId { get; set; }
        public int ModelId { get; set; }
        public int ColorId { get; set; }
    }
}
