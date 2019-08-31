using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using System.Net.Http;
using Microsoft.EntityFrameworkCore;
using SampleSPA.Data;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Logging;

namespace SampleSPA.Api.FunctionalTests.Support
{
    public class TestWebApplicationFactory : WebApplicationFactory<Startup>
    {
        public static TestWebApplicationFactory Instance;
        public static HttpClient Client;

        public static void Initialize()
        {
            Instance = new TestWebApplicationFactory();
            Client = Instance.CreateClient();

            using (var scope = Instance.Server.Host.Services.CreateScope())
            {
                var scopedServices = scope.ServiceProvider;

                var db = scopedServices.GetRequiredService<BloggingContext>();
                db.Database.EnsureDeleted();
                db.Database.Migrate();
            }
        }

        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.ConfigureServices(services =>
            {
                var serviceProvider = new ServiceCollection()
                    .BuildServiceProvider();
            })
            .ConfigureLogging(delegate (WebHostBuilderContext context, ILoggingBuilder logging)
            {
                logging.SetMinimumLevel(LogLevel.Trace);
            });
        }
    }
}
