using Newtonsoft.Json;

namespace SampleSPA.Api.Models
{
    [JsonObject(Title = "Blog")]
    public class BlogModel
    {
        public int Id { get; set; }
        public string Url { get; set; }
    }
}
