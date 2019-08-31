namespace SampleSPA.Data.Model
{
    public partial class Post
    {
        public int Id { get; set; }
        public int BlogId { get; set; }
        public string Content { get; set; }
        public string Title { get; set; }

        public Blog Blog { get; set; }
    }
}
