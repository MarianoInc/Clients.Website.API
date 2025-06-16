using Domain.Entities;

namespace Tests
{
    public class ClientTests
    {
        public void CanCreateClient()
        {
            var client = new Client { Name = "Test", Email = "test@test.com" };
            Assert.NotNull(client);
        }
    }
}