using Domain.Elements;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Services
{
    public class ElementsMemoryStorage
    {
        private IDictionary<string, ContentElement> theStorage = new Dictionary<string, ContentElement>();

        public void Add(ContentElement element)
        {
            theStorage[element.Hash] = element;
        }

        public ContentElement Get(string hash)
        {
            if (theStorage.ContainsKey(hash))
                return theStorage[hash];
            return null;
        }
    }
}
