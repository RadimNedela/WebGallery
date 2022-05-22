using Application.Databases;
using ApplicationTests.IoC;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;
using WebGalery.Domain.Databases.Factories;

namespace ApplicationTests.Databases
{
    [TestFixture]
    public class DatabaseDomainBuilderTests
    {
        [Test]
        public void BuildDomain_ValidDto_SetsTheName()
        {
            var fixture = new TestFixture();
            var builder = fixture.Build();

            var domain = builder.BuildDomain(fixture.BuildDto());

            Assert.That(domain.Name, Is.EqualTo("My new builder database"));
        }

        [Test]
        public void BuildDomain_ValidDto_HashIs40CharsLong()
        {
            var fixture = new TestFixture();
            var builder = fixture.Build();

            var domain = builder.BuildDomain(fixture.BuildDto());

            Assert.That(domain.Hash.Length, Is.EqualTo(40));
        }

        [Test]
        public void BuildDomain_ValidDto_UsedTwiceGeneratesDifferentHash()
        {
            var fixture = new TestFixture();
            var builder = fixture.Build();
            var dto = fixture.BuildDto();

            var domain1 = builder.BuildDomain(dto);
            var domain2 = builder.BuildDomain(dto);

            Assert.That(domain1.Hash, Is.Not.EqualTo(domain2.Hash));
        }

        private class TestFixture
        {
            public DatabaseDomainBuilder Build()
            {
                var builder = new DatabaseDomainBuilder(StaticInitializer.ServiceCollection.BuildServiceProvider().GetService<IDatabaseFactory>());

                return builder;
            }

            internal DatabaseDto BuildDto()
            {
                return new DatabaseDto() { Name = "My new builder database" };
            }
        }
    }
}
