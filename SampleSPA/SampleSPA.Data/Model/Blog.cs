using System.Collections.Generic;

namespace SampleSPA.Data.Model
{
    public partial class Blog
    {
        public Blog()
        {
            Post = new HashSet<Post>();
        }

        public int Id { get; set; }
        public string Url { get; set; }

        public ICollection<Post> Post { get; set; }
    }
}
