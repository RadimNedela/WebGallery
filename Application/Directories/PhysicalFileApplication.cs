using Domain.Dtos;
using Domain.Services;
using System;
using System.IO;

namespace Application.Directories
{
    public class PhysicalFileApplication
    {
        private readonly ElementsMemoryStorage _elementsMemoryStorage;

        public PhysicalFileApplication (ElementsMemoryStorage elementsMemoryStorage)
        {
            _elementsMemoryStorage = elementsMemoryStorage;
        }

        public BinaryContentDto GetContent(string hash)
        {
            var element = _elementsMemoryStorage.Get(hash);
            if (element == null)
            {
                // get it somehow from database 
                throw new NotImplementedException();
            }
            return element.ToBinaryContentDto();
        }
    }
}