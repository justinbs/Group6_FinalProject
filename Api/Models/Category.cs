namespace Api.Models
{
    public class Category
    {
        public int Id { get; set; }
        public string Name { get; set; } = "";

        // Navigation
        public List<Item> Items { get; set; } = new();
    }
}
