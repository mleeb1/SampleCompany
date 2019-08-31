using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using FluentAssertions;
using Newtonsoft.Json;
using SampleSPA.Api.Contracts;
using SampleSPA.Api.FunctionalTests.Support;
using SampleSPA.Api.Models;
using SampleSPA.Data.Model;
using Xunit;

namespace SampleSPA.Api.FunctionalTests.Controllers
{
    [Collection("Database Tests")]
    public class BlogControllerTests : ApiTestBase
    {
        [Fact]
        public async Task Get_WithInvalidUrlPath_ReturnsNotFoundResponse()
        {
            using (var response = await ExecuteGet($"/api/Blogs/xxx/yyy/zzz"))
            {
                response.StatusCode.Should().Be(HttpStatusCode.NotFound);

                var stringResponse = await response.Content.ReadAsStringAsync();
                stringResponse.Should().BeEmpty();
            }
        }

        [Fact]
        public async Task Get_WhenCalled_ReturnsExistingBlogInList()
        {
            var url = GetRandomUrl();
            var blog = new Blog {Url = url };
            SaveBlog(blog);

            using (var response = await ExecuteGet("/api/Blogs"))
            {
                response.EnsureSuccessStatusCode();

                var stringResponse = await response.Content.ReadAsStringAsync();
                var result = JsonConvert.DeserializeObject<List<BlogModel>>(stringResponse);

                result.Should().NotBeEmpty();
                result.Should().ContainEquivalentOf(new BlogModel { Id = blog.Id, Url = blog.Url});
            }
        }

        [Fact]
        public async Task GetById_WhenCalledWithValidId_ReturnsBlog()
        {
            var url = GetRandomUrl();
            var blog = new Blog { Url = url };
            blog = SaveBlog(blog);

            using (var response = await ExecuteGet($"/api/Blogs/{blog.Id}"))
            {
                response.EnsureSuccessStatusCode();

                var stringResponse = await response.Content.ReadAsStringAsync();
                var result = JsonConvert.DeserializeObject<BlogModel>(stringResponse);

                result.Should().NotBeNull();
                result.Should().Match<BlogModel>(b => b.Url == url);
            }
        }

        [Fact]
        public async Task Post_WhenBlogUrlIsEmpty_ReturnsBadRequestResponse()
        {
            var request = new BlogRequest { Url = "" };

            using (var response = await ExecutePost("/api/Blogs", request))
            {
                response.StatusCode.Should().Be(HttpStatusCode.BadRequest);

                var stringResponse = await response.Content.ReadAsStringAsync();
                stringResponse.Should().Be("{\"message\":\"Validation Errors\",\"errors\":[\"Blog is required\"]}");
            }
        }

        [Fact]
        public async Task Post_WhenBlogUrlIsValid_ReturnsAddedBlog()
        {
            var url = GetRandomUrl();
            var request = new BlogRequest { Url = url };

            using (var response = await ExecutePost("/api/Blogs", request))
            {
                response.EnsureSuccessStatusCode();

                var stringResponse = await response.Content.ReadAsStringAsync();
                var result = JsonConvert.DeserializeObject<BlogModel>(stringResponse);
                result.Should().NotBeNull();
                result.Should().Match<BlogModel>(b => b.Url == url);
            }
        }

        private static string GetRandomUrl()
        {
            return Guid.NewGuid().ToString();
        }
    }
}
