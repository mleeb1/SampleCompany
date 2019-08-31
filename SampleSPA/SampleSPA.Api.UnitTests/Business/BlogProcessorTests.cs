using System.Collections.Generic;
using System.Linq;
using Company.Common.Repository;
using FluentAssertions;
using Moq;
using SampleSPA.Api.Business;
using SampleSPA.Api.Models;
using SampleSPA.Data.Model;
using Xunit;

namespace SampleSPA.Api.UnitTests.Business
{
    public class BlogProcessorTests
    {
        private readonly Mock<IRepository<Blog>> _repository;
        private readonly IBlogProcessor _blogProcessor;

        public BlogProcessorTests()
        {
            _repository = new Mock<IRepository<Blog>>();
            _blogProcessor = new BlogProcessor(_repository.Object);
        }

        [Fact]
        public void GetAllBlogs_WhenExistingBlogFound_ReturnsBlog()
        {
            var blogInDatabase = new Blog {Id = 1, Post = null, Url = "http://www.company.com"};
            var expectedBlogModel = new BlogModel {Id = blogInDatabase.Id, Url = blogInDatabase.Url};

            _repository.Setup(library => library.FindAll())
                .Returns(new List<Blog>{ blogInDatabase }.AsQueryable());

            var actual = _blogProcessor.GetAllBlogs();

            actual.Should().NotBeEmpty()
                .And.HaveCount(1)
                .And.ContainItemsAssignableTo<BlogModel>()
                .And.ContainEquivalentOf(expectedBlogModel);
        }

        [Fact]
        public void GetById_WhenExistingBlogFound_ReturnsBlog()
        {
            var blogInDatabase = new Blog {Id = 1, Post = null, Url = "http://www.company.com"};
            var blogInDatabase2 = new Blog {Id = 2, Post = null, Url = "http://www.other.com"};
            var expectedBlogModel = new BlogModel {Id = blogInDatabase.Id, Url = blogInDatabase.Url};

            _repository.Setup(library => library.FindAll())
                .Returns(new List<Blog> {blogInDatabase, blogInDatabase2}.AsQueryable());

            var actual = _blogProcessor.GetBlog(blogInDatabase.Id);

            actual.Should().BeEquivalentTo(expectedBlogModel);
        }
    }
}
