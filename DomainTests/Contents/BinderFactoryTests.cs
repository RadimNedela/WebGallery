using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebGalery.Core.InfrastructureInterfaces;
using WebGalery.Domain.Contents.Factories;
using WebGalery.Infrastructure.FileServices;

namespace DomainTests.Contents
{
    internal class BinderFactoryTests
    {
        [Test]
        public void LoadDirectory_TestDirectory_Creates2ChildBinders()
        {
            BinderFactory binderFactory = BuildSut();

            var binder = binderFactory.LoadDirectory("TestPictures");

            Assert.That(binder.ChildBinders.Count, Is.EqualTo(2));
        }

        [Test]
        public void LoadDirectory_TestDirectory_Creates9Displayables()
        {
            BinderFactory binderFactory = BuildSut();

            var binder = binderFactory.LoadDirectory("TestPictures");

            Assert.That(binder.NumberOfDisplayables, Is.EqualTo(9));
        }

        [Test]
        public void LoadDirectory_TestDirectory_Has0DirectImages()
        {
            BinderFactory binderFactory = BuildSut();

            var binder = binderFactory.LoadDirectory("TestPictures");

            Assert.That(binder.Displayables.Count, Is.EqualTo(0));
        }

        private static BinderFactory BuildSut()
        {
            var directoryReader = new DirectoryMethods();
            var displayableFactory = new DisplayableFactory();
            var binderFactory = new BinderFactory(directoryReader, displayableFactory);
            return binderFactory;
        }
    }
}
