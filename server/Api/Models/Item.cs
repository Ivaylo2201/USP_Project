namespace Api.Models
{
    public class Item
    {
        public int Id { get; set; }

        public int PhoneId { get; set; }
        public required Phone Phone { get; set; }

        public int CartId { get; set; }
        public required Cart Cart { get; set; }

        public int Quantity { get; set; }
    }
}
