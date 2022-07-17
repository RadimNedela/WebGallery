using DomainTests;
using NUnit.Framework;
using WebGalery.Domain.Warehouses.Factories;

namespace WebGalery.Domain.Tests.Warehouses
{
    [TestFixture]
    public class DepositoryFactoryTests
    {
        [Test]
        public void BuildDomain_ValidDto_SetsTheName()
        {
            var fixture = new TestFixture();
            var builder = fixture.Build();

            var domain = builder.Build("My new builder database");

            Assert.That(domain.Name, Is.EqualTo("My new builder database"));
        }

        [Test]
        public void BuildDomain_ValidDto_HashIs40CharsLong()
        {
            var fixture = new TestFixture();
            var builder = fixture.Build();

            var domain = builder.Build("My new builder database");

            Assert.That(domain.Hash.Length, Is.EqualTo(40));
        }

        [Test]
        public void BuildDomain_ValidDto_UsedTwiceGeneratesDifferentHash()
        {
            var fixture = new TestFixture();
            var builder = fixture.Build();

            var domain1 = builder.Build("My new builder database");
            var domain2 = builder.Build("My new builder database");

            Assert.That(domain1.Hash, Is.Not.EqualTo(domain2.Hash));
        }
        private class TestFixture
        {
            public DepositoryFactory Build()
            {
                var factory = new ObjectMother().DepositoryFactory;

                return factory;
            }
        }
    }
}
