using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Company.Common;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using SampleSPA.Data;
using SampleSPA.Data.Model;

namespace SampleSPA.Api.FunctionalTests.Support
{
    public abstract class ApiTestBase
    {
        protected HttpClient Client;
        private IServiceScope _scope;

        public ApiTestBase()
        {
            Client = TestWebApplicationFactory.Client;
            _scope = TestWebApplicationFactory.Instance.Server.Host.Services.CreateScope();
        }

        protected T GetScopedService<T>()
        {
            return _scope.ServiceProvider.GetService<T>();
        }

        protected BloggingContext GetDbContext()
        {
            return (BloggingContext) GetScopedService<IDbContext>();
        }

        protected async Task<HttpResponseMessage> ExecuteGet(string url)
        {
            var request = new HttpRequestMessage(HttpMethod.Get, url);
            request.Headers.Add("Accept", "application/json");

            var response = await Client.SendAsync(request);
            return response;
        }

        protected async Task<HttpResponseMessage> ExecutePost(string url, object data)
        {
            var request = new HttpRequestMessage(HttpMethod.Post, url);
            request.Headers.Add("Accept", "application/json");
            request.Content = GetStringContent(data);

            var response = await Client.SendAsync(request);
            return response;
        }

        protected Blog SaveBlog(Blog blog)
        {
            var dbContext = GetDbContext();
            dbContext.Blogs.Add(blog);
            dbContext.SaveChanges();
            return blog;
        }

        private static HttpContent GetStringContent(object data)
        {
            return new StringContent(JsonConvert.SerializeObject(data), Encoding.Default, "application/json");
        }

        ~ApiTestBase()
        {
            _scope.Dispose();
        }
    }
}
