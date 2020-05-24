using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;
using WebApplication;
using WebApplication.Controllers;

namespace IntegrationTests.Directories
{
    [TestFixture]
    public class DirectoryContentTests
    {
        [Test]
        public void DirectoryContentApp_IsResolvable()
        {
            var confBuilder = new ConfigurationBuilder();
            var target = new Startup(confBuilder.Build());
            IServiceCollection services = new ServiceCollection();

            target.ConfigureServices(services);
            services.AddTransient<DirectoriesController>();

            var serviceProvider = services.BuildServiceProvider();

            var controller = serviceProvider.GetService<DirectoriesController>();
            Assert.IsNotNull(controller);
        }

        // [TestMethod]
        // public void ConfigureServices_RegistersDependenciesCorrectly()
        // {
        //     //  Arrange

        //     //  Setting up the stuff required for Configuration.GetConnectionString("DefaultConnection")
        //     Mock<IConfigurationSection> configurationSectionStub = new Mock<IConfigurationSection>();
        //     configurationSectionStub.Setup(x => x["DefaultConnection"]).Returns("TestConnectionString");
        //     Mock<Microsoft.Extensions.Configuration.IConfiguration> configurationStub = new Mock<Microsoft.Extensions.Configuration.IConfiguration>();
        //     configurationStub.Setup(x => x.GetSection("ConnectionStrings")).Returns(configurationSectionStub.Object);

        //     IServiceCollection services = new ServiceCollection();
        //     var target = new Startup(configurationStub.Object);

        //     //  Act

        //     target.ConfigureServices(services);
        //     //  Mimic internal asp.net core logic.
        //     services.AddTransient<TestController>();

        //     //  Assert

        //     var serviceProvider = services.BuildServiceProvider();

        //     var controller = serviceProvider.GetService<TestController>();
        //     Assert.IsNotNull(controller);
        // }
    }
}