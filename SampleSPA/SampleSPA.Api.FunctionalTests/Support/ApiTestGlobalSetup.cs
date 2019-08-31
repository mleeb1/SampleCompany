using System;
using Xunit;

namespace SampleSPA.Api.FunctionalTests.Support
{
    public class DatabaseFixture : IDisposable
    {
        public DatabaseFixture()
        {
            // ... initialize data in the test database ...
            TestWebApplicationFactory.Initialize();
        }

        public void Dispose()
        {
            // ... clean up test data from the database ...
        }
    }

    [CollectionDefinition("Database Tests")]
    public class DatabaseCollection : ICollectionFixture<DatabaseFixture>
    {
        // This class has no code, and is never created. Its purpose is simply
        // to be the place to apply [CollectionDefinition] and all the
        // ICollectionFixture<> interfaces.
    }
}
