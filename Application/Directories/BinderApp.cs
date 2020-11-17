using Domain.Dtos;
using Domain.Services;
using System;

namespace Application.Directories
{
    public class BinderApp
    {
        private readonly IContentElementRepository _elementsMemoryStorage;

        public BinderApp (IContentElementRepository elementsMemoryStorage)
        {
            _elementsMemoryStorage = elementsMemoryStorage;
        }

        public ContentInfoDto GetContent(string hash)
        {
            var element = _elementsMemoryStorage.Get(hash);
            if (element == null)
            {
                // get it somehow from database 
                throw new NotImplementedException();
            }
            return element.ToContentInfoDto();
        }
    }
}