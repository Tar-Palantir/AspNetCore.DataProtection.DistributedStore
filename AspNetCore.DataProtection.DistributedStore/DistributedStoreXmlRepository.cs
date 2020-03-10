using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using Microsoft.AspNetCore.DataProtection.Repositories;
using Microsoft.Extensions.Caching.Distributed;

namespace AspNetCore.DataProtection.DistributedStore
{
    public class DistributedStoreXmlRepository: IXmlRepository
    {
        private readonly IDistributedCache _cache;
        private readonly string _key;

        public DistributedStoreXmlRepository(
            IDistributedCache cache,
            string key)
        {
            _cache = cache ?? throw new ArgumentNullException(nameof(cache));

            _key = key;
        }

        public IReadOnlyCollection<XElement> GetAllElements()
        {
            var data = _cache.GetString(_key);
            if(!string.IsNullOrEmpty(data))
            {
                return XDocument.Parse(data).Root.Elements().ToList().AsReadOnly();
            }
            else
            {
                return new List<XElement>().AsReadOnly();
            }
        }

        public void StoreElement(XElement element, string friendlyName)
        {
            var data = _cache.GetString(_key);

            XDocument doc = string.IsNullOrEmpty(data) ?
                new XDocument(new XElement("keys")) : XDocument.Parse(data);
            doc.Root.Add(element);

            _cache.SetString(_key, doc.ToString());
        }
    }
}