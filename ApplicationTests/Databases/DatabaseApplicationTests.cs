﻿using Application.Databases;
using ApplicationTests.IoC;
using Microsoft.Extensions.DependencyInjection;
using NSubstitute;
using NUnit.Framework;
using System;
using WebGalery.Database.Databases;
using WebGalery.Domain.Warehouses;
using WebGalery.Domain.Warehouses.Factories;

namespace ApplicationTests.Databases
{
    [TestFixture]
    public class DatabaseApplicationTests
    {
        [Test]
        public void CreateDatabase_ValidDto_CallsSaveChanges()
        {
            var fixture = new TestFixture();
            var application = fixture.WithDatabaseMock().Build();

            application.CreateNewDatabase(fixture.BuildDto());

            fixture.GaleryDatabase.Received().SaveChanges();
        }

        [Test]
        public void CreateDatabase_ValidDto_AddsDomainObjectToDatabase()
        {
            var fixture = new TestFixture();
            var application = fixture.WithDatabaseMock().Build();

            application.CreateNewDatabase(fixture.BuildDto());

            fixture.GaleryDatabase.Depositories.Received().Add(Arg.Any<Depository>());
        }


        [Test]
        public void CreateDatabase_ValidInput_ReturnsDtoWithAllDataFilledIn()
        {
            var fixture = new TestFixture().WithDatabaseMock();
            var application = fixture.Build();
            var dto = fixture.BuildDto();

            // act
            var returnedDto = application.CreateNewDatabase(dto);

            // assert
            Assert.That(returnedDto.Name.Length, Is.GreaterThan(10));
            Assert.That(returnedDto.Hash.Length, Is.EqualTo(40));
        }

        [Test]
        [Category("DatabaseTests")]
        public void DatabaseCRUD_Works()
        {
            var fixture = new TestFixture().WithRealDatabase();
            var application = fixture.Build();
            var dto = fixture.BuildDto();

            // act
            var returnedDto = application.CreateNewDatabase(dto);

        }


        private class TestFixture
        {
            public IGaleryDatabase GaleryDatabase { get; private set; }
            public IDepositoryFactory DatabaseFactory { get; private set; }

            private ServiceProvider _serviceProvider;

            public TestFixture()
            {
                _serviceProvider = StaticInitializer.ServiceCollection.BuildServiceProvider();
            }

            public DatabaseApplication Build()
            {

                DatabaseApplication application = new(
                    GaleryDatabase ?? throw new NotImplementedException($"Use {nameof(WithRealDatabase)} or {nameof(WithDatabaseMock)}"),
                    DatabaseFactory ?? _serviceProvider.GetService<IDepositoryFactory>()
                    );

                return application;
            }

            public DatabaseDto BuildDto()
            {
                return new DatabaseDto();
            }

            public TestFixture WithDatabaseMock()
            {
                GaleryDatabase = Substitute.For<IGaleryDatabase>();
                return this;
            }

            public TestFixture WithRealDatabase()
            {
                GaleryDatabase = _serviceProvider.GetService<IGaleryDatabase>();
                return this;
            }

            public TestFixture WithDomainBuilderMock()
            {
                DatabaseFactory = Substitute.For<IDepositoryFactory>();
                return this;
            }
        }
    }
}
