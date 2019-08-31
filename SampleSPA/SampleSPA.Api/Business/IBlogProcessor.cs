using System.Collections.Generic;
using SampleSPA.Api.Contracts;
using SampleSPA.Api.Models;

namespace SampleSPA.Api.Business
{
    public interface IBlogProcessor
    {
        BlogModel GetBlog(int id);
        IEnumerable<BlogModel> GetAllBlogs();
        BlogModel AddBlog(BlogRequest request);
    }
}
