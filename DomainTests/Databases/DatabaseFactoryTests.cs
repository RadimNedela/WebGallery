using DomainTests;
using NUnit.Framework;
using WebGalery.Domain.Databases.Factories;

namespace ApplicationTests.Databases
{
    [TestFixture]
    public class DatabaseFactoryTests
    {
        [Test]
        public void BuildDomain_ValidDto_SetsTheName()
        {
            var fixture = new TestFixture();
            var builder = fixture.Build();

            var domain = builder.Create("My new builder database");

            Assert.That(domain.Name, Is.EqualTo("My new builder database"));
        }

        [Test]
        public void BuildDomain_ValidDto_HashIs40CharsLong()
        {
            var fixture = new TestFixture();
            var builder = fixture.Build();

            var domain = builder.Create("My new builder database");

            Assert.That(domain.Hash.Length, Is.EqualTo(40));
        }

        [Test]
        public void BuildDomain_ValidDto_UsedTwiceGeneratesDifferentHash()
        {
            var fixture = new TestFixture();
            var builder = fixture.Build();

            var domain1 = builder.Create("My new builder database");
            var domain2 = builder.Create("My new builder database");

            Assert.That(domain1.Hash, Is.Not.EqualTo(domain2.Hash));
        }

        private class TestFixture
        {
            public DatabaseFactory Build()
            {
                var factory = new ObjectMother().DatabaseFactory;

                return factory;
            }
        }
    }
}
