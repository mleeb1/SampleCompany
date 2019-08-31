using System.Collections.Generic;
using System.Linq;
using Company.Common.Repository;
using SampleSPA.Api.Contracts;
using SampleSPA.Api.Models;
using SampleSPA.Data.Model;

namespace SampleSPA.Api.Business
{
    public class BlogProcessor : IBlogProcessor
    {
        private readonly IRepository<Blog> _repository;

        public BlogProcessor(IRepository<Blog> repository)
        {
            _repository = repository;
        }

        public BlogModel GetBlog(int id)
        {
            var blog = _repository.FindAll()
                .Where(b => b.Id == id)
                .AsEnumerable()
                .Select(BlogEntityToApiBlog)
                .FirstOrDefault();

            return blog;
        }

        public IEnumerable<BlogModel> GetAllBlogs()
        {
            var blogs = _repository.FindAll()
                .AsEnumerable()
                .Select(BlogEntityToApiBlog)
                .ToList();

            return blogs;
        }

        public BlogModel AddBlog(BlogRequest request)
        {
            var blog = new Blog {Url = request.Url};
            blog = _repository.Add(blog);
            return new BlogModel {Id = blog.Id, Url = blog.Url};
        }

        private static BlogModel BlogEntityToApiBlog(Blog arg)
        {
            // TODO: Use AutoMapper
            return new BlogModel
            {
                Id = arg.Id,
                Url = arg.Url
            };
        }
    }
}
