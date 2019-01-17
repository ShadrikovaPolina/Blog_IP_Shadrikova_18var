namespace Blog.Models
{
    public class Post
    {
        public int Id { get; set; }

        public string Date { get; set; }

        public string Name { get; set; }

        public string Content { get; set; }

        public int? CategoryId { get; set; }
    }
}