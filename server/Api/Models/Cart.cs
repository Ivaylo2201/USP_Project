namespace Api.Models
{
    public class Cart
    {
        public int Id { get; set; }

        public int UserId { get; set; }
        public required User User { get; set; }

        public List<Item> Items { get; set; } = [];
    }
}
