using Domain.Elements;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Services
{
    public class ElementsMemoryStorage
    {
        private readonly IDictionary<string, ContentElement> _theStorage = new Dictionary<string, ContentElement>();

        public void Add(ContentElement element)
        {
            _theStorage[element.Hash] = element;
        }

        public ContentElement Get(string hash)
        {
            if (_theStorage.ContainsKey(hash))
                return _theStorage[hash];
            return null;
        }
    }
}
