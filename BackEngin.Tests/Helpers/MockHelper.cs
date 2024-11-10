using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Moq;

namespace BackEngin.Tests.Helpers
{
    public static class MockHelper
    {
        public static Mock<UserManager<TUser>> MockUserManager<TUser>() where TUser : class
        {
            var store = new Mock<IUserStore<TUser>>();
            return new Mock<UserManager<TUser>>(store.Object, null, null, null, null, null, null, null, null);
        }

        public static Mock<IConfiguration> MockConfiguration()
        {
            var mockConfig = new Mock<IConfiguration>();

            // Create a mock section for "JwtSettings"
            var mockJwtSection = new Mock<IConfigurationSection>();
            mockJwtSection.Setup(x => x["Issuer"]).Returns("TestIssuer");
            mockJwtSection.Setup(x => x["Audience"]).Returns("TestAudience");
            mockJwtSection.Setup(x => x["SecretKey"]).Returns("ThisIsASufficientlyLongSecretKey12345");

            // Setup GetSection to return the mocked JwtSettings section
            mockConfig.Setup(x => x.GetSection("JwtSettings")).Returns(mockJwtSection.Object);

            return mockConfig;
        }
    }
}
