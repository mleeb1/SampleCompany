using System.Linq;
using Microsoft.AspNetCore.Mvc;
using SampleSPA.Api.Business;
using SampleSPA.Api.Contracts;

namespace SampleSPA.Api.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    public class BlogsController : ControllerBase
    {
        private readonly IBlogProcessor _blogProcessor;

        public BlogsController(IBlogProcessor blogProcessor)
        {
            _blogProcessor = blogProcessor;
        }

        /// <response code="200">Returns a list of all blogs</response>
        /// <response code="500">Failed to retrieve</response>
        /// <remarks>
        /// Sample request:
        ///
        ///     GET api/blogs
        /// 
        /// </remarks>
        // GET api/blogs
        [HttpGet]
        [ProducesResponseType(200)]
        [ProducesResponseType(500)]
        public IActionResult Get()
        {
            var blogs = _blogProcessor.GetAllBlogs();
            return new JsonResult(blogs);
        }

        /// <param name="id">Blog Id</param>
        /// <response code="200">Returns a blog for the given Id</response>
        /// <response code="500">Failed to retrieve</response>
        /// <remarks>
        /// Sample request:
        ///
        ///     GET api/blogs/5
        /// 
        /// </remarks>
        // GET api/blogs/5
        [HttpGet("{id}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(500)]
        public IActionResult Get(int id)
        {
            var blog = _blogProcessor.GetBlog(id);
            return new JsonResult(blog);
        }

        // POST api/blogs
        [HttpPost]
        [ProducesResponseType(200)]
        [ProducesResponseType(500)]
        public IActionResult Post([FromBody] BlogRequest request)
        {
            var blog = _blogProcessor.AddBlog(request);
            return new JsonResult(blog);
        }
    }
}
